using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace CSharpDesctop.Services
{
    public static class RoslynCompiler
    {
        private enum CodeMode
        {
            Snippet,        
            WithMethods,    
            FullClass       
        }

        public static string CompileAndRun(string userSnippet, out bool isSuccess)
        {
            CodeMode mode = DetectMode(userSnippet);

            string fullCode = mode switch
            {
                CodeMode.Snippet => $@"
                    using System;
                    using System.Collections.Generic;
                    using System.Linq;
                    public class Program
                    {{
                        public static void Main()
                        {{
                            {userSnippet}
                        }}
                    }}",

                CodeMode.WithMethods => $@"
                    using System;
                    using System.Collections.Generic;
                    using System.Linq;
                    public class Program
                    {{
                        {userSnippet}
                    }}",

                CodeMode.FullClass => $@"
                    using System;
                    using System.Collections.Generic;
                    using System.Linq;
                    {userSnippet}",

                _ => userSnippet
            };

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(fullCode);
            string assemblyName = Path.GetRandomFileName();

            var references = new List<MetadataReference>();
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!asm.IsDynamic && !string.IsNullOrWhiteSpace(asm.Location))
                    references.Add(MetadataReference.CreateFromFile(asm.Location));
            }

            
            var outputKind = mode == CodeMode.FullClass
                ? OutputKind.ConsoleApplication
                : OutputKind.DynamicallyLinkedLibrary;

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(outputKind)
            );

            using var ms = new MemoryStream();
            EmitResult result = compilation.Emit(ms);

            if (!result.Success)
            {
                
                string fallbackResult = TryFallback(userSnippet, mode, out isSuccess);
                if (isSuccess) return fallbackResult;

                var errors = new StringBuilder();
                errors.AppendLine("❌ Ошибка компиляции:\r\n");
                foreach (Diagnostic d in result.Diagnostics)
                    if (d.Severity == DiagnosticSeverity.Error)
                        errors.AppendLine(d.GetMessage());

                isSuccess = false;
                return errors.ToString();
            }

            ms.Seek(0, SeekOrigin.Begin);
            Assembly assembly = Assembly.Load(ms.ToArray());

            
            Type type = assembly.GetType("Program")
                        ?? FindTypeWithMain(assembly);

            if (type == null)
            {
                isSuccess = false;
                return "❌ Не найден класс с методом Main.";
            }

            MethodInfo method = type.GetMethod("Main",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (method == null)
            {
                isSuccess = false;
                return "❌ Метод Main не найден.";
            }

            TextWriter oldOut = Console.Out;
            using var sw = new StringWriter();
            Console.SetOut(sw);
            try
            {
                
                var parameters = method.GetParameters().Length > 0
                    ? new object[] { new string[0] }
                    : null;

                method.Invoke(null, parameters);
                isSuccess = true;
                return sw.ToString();
            }
            catch (TargetInvocationException ex)
            {
                isSuccess = false;
                return $"❌ Ошибка выполнения:\r\n{ex.InnerException?.Message}";
            }
            finally
            {
                Console.SetOut(oldOut);
            }
        }

        

        private static CodeMode DetectMode(string code)
        {
            string trimmed = code.Trim();

            
            if (Regex.IsMatch(trimmed, @"\bclass\s+\w+", RegexOptions.Multiline))
                return CodeMode.FullClass;

            
            bool hasMethods = Regex.IsMatch(trimmed,
                @"(void|int|string|bool|double|float|char|long|object|var)\s+\w+\s*\(.*\)\s*(=>|\{)",
                RegexOptions.Multiline);

            bool hasProperties = Regex.IsMatch(trimmed,
                @"\b(public|private|protected|static|readonly)\b",
                RegexOptions.Multiline);

            if (hasMethods || hasProperties)
                return CodeMode.WithMethods;

            
            return CodeMode.Snippet;
        }

        

        private static string TryFallback(string userSnippet, CodeMode failedMode,
            out bool isSuccess)
        {
            
            if (failedMode == CodeMode.Snippet)
                return CompileWithMode(userSnippet, CodeMode.WithMethods, out isSuccess);

            
            if (failedMode == CodeMode.WithMethods)
                return CompileWithMode(userSnippet, CodeMode.Snippet, out isSuccess);

            isSuccess = false;
            return string.Empty;
        }

        private static string CompileWithMode(string userSnippet, CodeMode mode,
            out bool isSuccess)
        {
            
            string patched = mode == CodeMode.Snippet
                ? $@"using System; using System.Collections.Generic; using System.Linq;
                     public class Program {{ public static void Main() {{ {userSnippet} }} }}"
                : $@"using System; using System.Collections.Generic; using System.Linq;
                     public class Program {{ {userSnippet} }}";

            var tree = CSharpSyntaxTree.ParseText(patched);
            var refs = new List<MetadataReference>();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                if (!a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                    refs.Add(MetadataReference.CreateFromFile(a.Location));

            var comp = CSharpCompilation.Create(
                Path.GetRandomFileName(), new[] { tree }, refs,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();
            var result = comp.Emit(ms);
            if (!result.Success) { isSuccess = false; return string.Empty; }

            ms.Seek(0, SeekOrigin.Begin);
            var asm = Assembly.Load(ms.ToArray());
            var type = asm.GetType("Program") ?? FindTypeWithMain(asm);
            var method = type?.GetMethod("Main",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (method == null) { isSuccess = false; return string.Empty; }

            var oldOut = Console.Out;
            using var sw = new StringWriter();
            Console.SetOut(sw);
            try
            {
                var p = method.GetParameters().Length > 0 ? new object[] { new string[0] } : null;
                method.Invoke(null, p);
                isSuccess = true;
                return sw.ToString();
            }
            catch { isSuccess = false; return string.Empty; }
            finally { Console.SetOut(oldOut); }
        }

        private static Type FindTypeWithMain(Assembly assembly)
        {
            foreach (var t in assembly.GetTypes())
                if (t.GetMethod("Main",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static) != null)
                    return t;
            return null;
        }
    }
}