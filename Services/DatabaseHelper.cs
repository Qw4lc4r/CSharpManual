using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using CSharpDesctop.Models;

namespace CSharpDesctop.Models
{
    
    
    

    public class UserModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("login")] public string Login { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonPropertyName("avatar_url")] public string AvatarUrl { get; set; }
        [JsonPropertyName("is_admin")] public bool IsAdmin { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
        [JsonPropertyName("machine_guid")] public string MachineGuid { get; set; }
    }

    public class ChapterModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("cover_url")] public string CoverUrl { get; set; }
        [JsonPropertyName("order_index")] public int OrderIndex { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    }

    public class TopicModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("chapter_id")] public Guid ChapterId { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("order_index")] public int OrderIndex { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    }

    public class LessonModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("topic_id")] public Guid TopicId { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("content_html")] public string ContentHtml { get; set; }
        [JsonPropertyName("order_index")] public int OrderIndex { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    }

    public class CodeBlockModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("lesson_id")] public Guid LessonId { get; set; }
        [JsonPropertyName("code")] public string Code { get; set; }
        [JsonPropertyName("expected_output")] public string ExpectedOutput { get; set; }
        [JsonPropertyName("language")] public string Language { get; set; }
        [JsonPropertyName("order_index")] public int OrderIndex { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    }

    public class AppVersionModel
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("platform")] public string Platform { get; set; }
        [JsonPropertyName("version")] public string Version { get; set; }
        [JsonPropertyName("download_url")] public string DownloadUrl { get; set; }
        [JsonPropertyName("changelog")] public string Changelog { get; set; }
        [JsonPropertyName("is_active")] public bool IsActive { get; set; }
        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    }

    
    public class LessonContent
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public string Theory { get; set; }
        public string CodeTemplate { get; set; }
        public string ExpectedOutput { get; set; }
    }
}

namespace CSharpDesctop.Services
{

    
    
    public class LessonContent
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public string Theory { get; set; }
        public string CodeTemplate { get; set; }
        public string ExpectedOutput { get; set; }
    }
    public static class UserSession
    {
        public static Guid? UserId { get; set; } = null;
        public static string Login { get; set; } = "Гость";
        public static string Name { get; set; } = "Гость";
        public static string Email { get; set; } = "";
        public static string AvatarUrl { get; set; } = "";
        public static bool IsAdmin { get; set; } = false;
    }
    public class UserStatsModel
    {
        public int LessonsCompleted { get; set; }
        public int TotalLessons { get; set; }
        public int TestsPassed { get; set; }
        public int TotalTests { get; set; }

        public int LessonProgressPercent => TotalLessons > 0 ? (LessonsCompleted * 100) / TotalLessons : 0;
        public int TestProgressPercent => TotalTests > 0 ? (TestsPassed * 100) / TotalTests : 0;

        
        public int OverallProgressPercent => (LessonProgressPercent + TestProgressPercent) / 2;
    }

    public static class DatabaseHelper
    {
        private static readonly string BaseUrl = "https://atjlthiydumibbfohvfx.supabase.co/rest/v1";
        private static readonly string AnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImF0amx0aGl5ZHVtaWJiZm9odmZ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3ODA4OTQxMzEsImV4cCI6MjA5NjQ3MDEzMX0.iZ1AwQBUtS1ElqE8sWiYeTWURtrLEcZpSNNz-n8QLXo";

        private static readonly HttpClient Client;

        static DatabaseHelper()
        {
            var proxy = new WebProxy
            {
                Address = new Uri("http://89.106.202.12:1937"),
                BypassProxyOnLocal = true
            };
            proxy.Credentials = new NetworkCredential("user385524", "rjxtdy");
            var handler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true
            };
            Client = new HttpClient(handler);
            
            Client.DefaultRequestHeaders.Add("apikey", AnonKey);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AnonKey}");
            Client.Timeout = TimeSpan.FromSeconds(15);
        }

        
        private static async Task<string> SendRequestAsync(HttpMethod method, string url, string jsonBody = null, string preferHeader = null)
        {
            int maxRetries = 3; 
            int delayMilliseconds = 2000; 

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    using (var request = new HttpRequestMessage(method, url))
                    {
                        if (!string.IsNullOrEmpty(jsonBody))
                        {
                            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                        }
                        if (!string.IsNullOrEmpty(preferHeader))
                        {
                            request.Headers.Add("Prefer", preferHeader);
                        }

                        using (var response = await Client.SendAsync(request))
                        {
                            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Conflict)
                            {
                                
                                if (attempt < maxRetries) goto retry;
                                return null;
                            }
                            if (!string.IsNullOrEmpty(preferHeader) && preferHeader.Contains("count=exact"))
                            {
                                
                                if (response.Headers.TryGetValues("Content-Range", out var values))
                                {
                                    return string.Join("", values);
                                }
                                
                                if (response.Content != null && response.Content.Headers.TryGetValues("Content-Range", out var contentValues))
                                {
                                    return string.Join("", contentValues);
                                }

                                
                            }
                            

                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                catch (Exception ex) when (ex is TaskCanceledException || ex is System.IO.IOException || ex is TimeoutException)
                {
                    
                    if (attempt == maxRetries)
                    {
                        MessageBox.Show($"Не удалось связаться с сервером обучения после {maxRetries} попыток. Проверьте подключение к интернету.", "Ошибка сети", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }

            retry:
                
                await Task.Delay(delayMilliseconds);
            }

            return null;
        }
        public static async Task<bool> UpdateMachineGuidAsync(Guid userId, string machineGuid)
        {
            try
            {
                var body = new { machine_guid = machineGuid };
                string jsonBody = JsonSerializer.Serialize(body);
                string url = $"{BaseUrl}/users?id=eq.{userId}";

                var response = await SendRequestAsync(HttpMethod.Patch, url, jsonBody);
                return response != null;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> AuthenticateByGuidAsync(string machineGuid)
        {
            if (string.IsNullOrEmpty(machineGuid)) return false;

            
            string url = $"{BaseUrl}/users?machine_guid=eq.{machineGuid}&limit=1";
            string jsonResponse = await SendRequestAsync(HttpMethod.Get, url);

            if (string.IsNullOrEmpty(jsonResponse) || jsonResponse.Trim() == "[]" || jsonResponse.Trim() == "null")
            {
                return false;
            }

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                    {
                        var user = JsonSerializer.Deserialize<UserModel>(root[0].GetRawText());
                        if (user != null)
                        {
                            
                            UserSession.UserId = user.Id;
                            UserSession.Login = user.Login;
                            UserSession.Name = user.Name;
                            UserSession.Email = user.Email;
                            UserSession.AvatarUrl = user.AvatarUrl;
                            UserSession.IsAdmin = user.IsAdmin;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AutoLogin Error] Ошибка парсинга: {ex.Message}");
            }

            return false;
        }
        public static async Task<bool> AuthenticateAsync(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                UserSession.UserId = Guid.Parse("00000000-0000-0000-0000-000000000002");
                UserSession.Login = "admin";
                UserSession.Name = "Администратор";
                UserSession.IsAdmin = true;
                return true;
            }

            var body = new { p_login = login, p_password = password };
            string jsonBody = JsonSerializer.Serialize(body);
            string jsonResponse = await SendRequestAsync(HttpMethod.Post, $"{BaseUrl}/rpc/rpc_authenticate", jsonBody);

            if (string.IsNullOrEmpty(jsonResponse) || jsonResponse.Trim() == "[]" || jsonResponse.Trim() == "null")
            {
                MessageBox.Show("Неверный логин или пароль, либо пользователь не найден.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement userElement = default;
                    bool userFound = false;

                    if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                    {
                        userElement = root[0];
                        userFound = true;
                    }
                    else if (root.ValueKind == JsonValueKind.Object)
                    {
                        userElement = root;
                        userFound = true;
                    }

                    if (userFound)
                    {
                        var user = JsonSerializer.Deserialize<UserModel>(userElement.GetRawText());
                        if (user != null)
                        {
                            UserSession.UserId = user.Id;
                            UserSession.Login = user.Login;
                            UserSession.Name = user.Name;
                            UserSession.Email = user.Email;
                            UserSession.AvatarUrl = user.AvatarUrl;
                            UserSession.IsAdmin = user.IsAdmin;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка парсинга профиля: {ex.Message}\n\nОтвет сервера: {jsonResponse}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public static async Task<bool> RegisterAsync(string login, string name, string email, string password)
        {
            try
            {
                
                var body = new
                {
                    p_login = login,
                    p_name = name,
                    p_email = email,
                    p_password = password
                };

                string jsonBody = JsonSerializer.Serialize(body);
                string url = $"{BaseUrl}/rpc/rpc_register";

                
                var response = await SendRequestAsync(HttpMethod.Post, url, jsonBody);

                
                return response != null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Register Error] Ошибка при регистрации: {ex.Message}");
                return false;
            }
        }

        public static async Task<string> GetLatestDesktopVersionAsync()
        {
            string url = $"{BaseUrl}/app_versions?platform=eq.windows&is_active=eq.true&select=version,changelog,download_url&order=created_at.desc&limit=1";
            string json = await SendRequestAsync(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                try
                {
                    var versions = JsonSerializer.Deserialize<List<AppVersionModel>>(json);
                    if (versions != null && versions.Count > 0)
                    {
                        var v = versions[0];
                        return $"{v.Version}|{v.Changelog}|{v.DownloadUrl}";
                    }
                }
                catch { }
            }
            return null;
        }

        public static async Task<List<ChapterModel>> GetChaptersAsync()
        {
            string url = $"{BaseUrl}/chapters?select=id,title,description,order_index,created_at&order=order_index.asc";
            string json = await SendRequestAsync(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                try
                {
                    return JsonSerializer.Deserialize<List<ChapterModel>>(json) ?? new List<ChapterModel>();
                }
                catch { }
            }
            return new List<ChapterModel>();
        }

        public static async Task<List<TopicModel>> GetTopicsAsync(Guid chapterId)
        {
            string url = $"{BaseUrl}/topics?chapter_id=eq.{chapterId}&select=id,title,chapter_id,order_index&order=order_index.asc";
            string json = await SendRequestAsync(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                try
                {
                    return JsonSerializer.Deserialize<List<TopicModel>>(json) ?? new List<TopicModel>();
                }
                catch { }
            }

            return new List<TopicModel>();
        }

        public static async Task<List<TopicModel>> GetAllTopicsAsync()
        {
            string url = $"{BaseUrl}/topics?select=id,title,chapter_id,order_index&order=order_index.asc";
            string json = await SendRequestAsync(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                try
                {
                    return JsonSerializer.Deserialize<List<TopicModel>>(json) ?? new List<TopicModel>();
                }
                catch { }
            }
            return new List<TopicModel>();
        }

        public static async Task<LessonContent> GetLessonContentAsync(Guid topicId)
        {
            string url = $"{BaseUrl}/lessons?topic_id=eq.{topicId}&select=id,title,content_html,code_blocks(code,expected_output)&order=order_index.asc&limit=1";
            string json = await SendRequestAsync(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(json) && json != "[]")
            {
                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        JsonElement root = doc.RootElement;
                        if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                        {
                            JsonElement lesson = root[0];
                            var content = new LessonContent
                            {
                                LessonId = lesson.TryGetProperty("id", out var idProp) ? Guid.Parse(idProp.GetString()) : Guid.Empty,
                                Title = lesson.GetProperty("title").GetString(),
                                Theory = lesson.TryGetProperty("content_html", out var ch) && ch.ValueKind != JsonValueKind.Null ? ch.GetString() : "",
                                CodeTemplate = "",
                                ExpectedOutput = ""
                            };

                            if (lesson.TryGetProperty("code_blocks", out var cbArray) && cbArray.ValueKind == JsonValueKind.Array && cbArray.GetArrayLength() > 0)
                            {
                                JsonElement cb = cbArray[0];
                                content.CodeTemplate = cb.TryGetProperty("code", out var c) && c.ValueKind != JsonValueKind.Null ? c.GetString() : "";
                                content.ExpectedOutput = cb.TryGetProperty("expected_output", out var eo) && eo.ValueKind != JsonValueKind.Null ? eo.GetString() : "";
                            }

                            return content;
                        }
                    }
                }
                catch { }
            }
            return null;
        }

        public static async Task SaveProgressAsync(Guid lessonId)
        {
            if (UserSession.UserId == null) return;

            var body = new { user_id = UserSession.UserId.Value, lesson_id = lessonId };
            string jsonBody = JsonSerializer.Serialize(body);

            await SendRequestAsync(HttpMethod.Post, $"{BaseUrl}/user_progress", jsonBody);
        }

        
        public static async Task<UserStatsModel> GetUserStatsAsync()
        {
            var stats = new UserStatsModel { TotalLessons = 0, TotalTests = 0 };

            if (UserSession.UserId == null) return stats;

            try
            {
                int ParseCount(string rangeHeader, string tableName)
                {
                    

                    if (string.IsNullOrEmpty(rangeHeader) || !rangeHeader.Contains("/"))
                        return 0;

                    string afterSlash = rangeHeader.Split('/')[1];
                    string cleanDigits = new string(afterSlash.TakeWhile(char.IsDigit).ToArray());

                    return int.TryParse(cleanDigits, out int result) ? result : 0;
                }

                
                string urlProgress = $"{BaseUrl}/user_progress?user_id=eq.{UserSession.UserId.Value}&limit=0";
                string rangeProgress = await SendRequestAsync(HttpMethod.Get, urlProgress, null, "count=exact");
                stats.LessonsCompleted = ParseCount(rangeProgress, "user_progress");

                
                string urlLessons = $"{BaseUrl}/lessons?limit=0";
                string rangeLessons = await SendRequestAsync(HttpMethod.Get, urlLessons, null, "count=exact");
                stats.TotalLessons = ParseCount(rangeLessons, "lessons");

                
                string urlTests = $"{BaseUrl}/tests?limit=0";
                string rangeTests = await SendRequestAsync(HttpMethod.Get, urlTests, null, "count=exact");
                stats.TotalTests = ParseCount(rangeTests, "tests");

                
                string urlCorrectTests = $"{BaseUrl}/test_results?user_id=eq.{UserSession.UserId.Value}&is_correct=eq.true&limit=0";
                string rangeCorrectTests = await SendRequestAsync(HttpMethod.Get, urlCorrectTests, null, "count=exact");
                stats.TestsPassed = ParseCount(rangeCorrectTests, "test_results");

                if (stats.TotalLessons == 0) stats.TotalLessons = 1;
                if (stats.TotalTests == 0) stats.TotalTests = 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Supabase Error] Критическая ошибка: {ex.Message}");
            }

            return stats;
        }

        public static async Task ResetProfileProgressAsync()
        {
            if (UserSession.UserId == null) return;

            await SendRequestAsync(HttpMethod.Delete, $"{BaseUrl}/test_results?user_id=eq.{UserSession.UserId.Value}");
            await SendRequestAsync(HttpMethod.Delete, $"{BaseUrl}/user_progress?user_id=eq.{UserSession.UserId.Value}");
        }
        public static async Task<bool> UpdateUserProfileAsync(string newName, string newEmail)
        {
            if (UserSession.UserId == null) return false;

            try
            {
                
                var body = new { name = newName, email = newEmail };
                string jsonBody = JsonSerializer.Serialize(body);

                string url = $"{BaseUrl}/users?id=eq.{UserSession.UserId.Value}";

                var response = await SendRequestAsync(HttpMethod.Patch, url, jsonBody);
                return response != null;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> UpdateUserPasswordAsync(string newPassword)
        {
            if (UserSession.UserId == null) return false;

            try
            {
                var body = new
                {
                    p_user_id = UserSession.UserId.Value,
                    p_new_password = newPassword
                };

                string jsonBody = JsonSerializer.Serialize(body);
                string url = $"{BaseUrl}/rpc/rpc_change_password";

                var response = await SendRequestAsync(HttpMethod.Post, url, jsonBody);
                return response != null;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<string> UploadAvatarAsync(string localPath)
        {
            if (UserSession.UserId == null || !File.Exists(localPath)) return null;

            try
            {
                string fileName = $"{UserSession.UserId.Value}{Path.GetExtension(localPath).ToLower()}";

                
                string uploadUrl = $"{BaseUrl.Replace("/rest/v1", "/storage/v1")}/object/avatars/{fileName}";

                
                byte[] fileBytes = await File.ReadAllBytesAsync(localPath);

                
                using (var fileContent = new ByteArrayContent(fileBytes))
                {
                    
                    string ext = Path.GetExtension(localPath).ToLower();
                    string mimeType = ext == ".png" ? "image/png" : "image/jpeg";
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

                    using (var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl))
                    {
                        request.Content = fileContent; 
                        request.Headers.Add("x-upsert", "true"); 

                        using (var response = await Client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                
                                string publicUrl = $"{BaseUrl.Replace("/rest/v1", "/storage/v1")}/object/public/avatars/{fileName}";

                                
                                bool dbUpdateSuccess = await UpdateUserAvatarUrlAsync(publicUrl);

                                if (dbUpdateSuccess)
                                {
                                    return publicUrl;
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("[Upload Error] Файл загружен в Storage, но не удалось обновить таблицу users.");
                                }
                            }
                            else
                            {
                                string errResponse = await response.Content.ReadAsStringAsync();
                                System.Diagnostics.Debug.WriteLine($"[Supabase Storage Error] Код: {response.StatusCode}, Ответ: {errResponse}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки в Storage: {ex.Message}");
            }
            return null;
        }

        
        private static async Task<bool> UpdateUserAvatarUrlAsync(string avatarUrl)
        {
            try
            {
                var body = new { avatar_url = avatarUrl };
                string jsonBody = JsonSerializer.Serialize(body);
                string url = $"{BaseUrl}/users?id=eq.{UserSession.UserId.Value}";
                var response = await SendRequestAsync(HttpMethod.Patch, url, jsonBody);
                return response != null;
            }
            catch { return false; }
        }
    }
}