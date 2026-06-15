using System;
using System.Drawing;
using System.Windows.Forms;
using CSharpDesctop.Models;
using CSharpDesctop.Services;
using FastColoredTextBoxNS;

namespace CSharpDesctop.Forms
{
    public partial class LessonForm : Form
    {
        private Guid _topicId;
        private Guid _actualLessonId;
        private string _expectedOutput = "";

        public LessonForm(Guid topicId, string topicTitle)
        {
            InitializeComponent();

            _topicId = topicId;
            this.Text = topicTitle;

            
            fctbEditor.Language = Language.CSharp;

            
            splitContainerMain.Panel1.Resize += (s, e) => ResizeLeftPanel();
            splitContainerMain.Panel2.Resize += (s, e) => ResizeRightPanel();
            Shown += (s, e) => { ResizeLeftPanel(); ResizeRightPanel(); };

            LoadLessonContent();
        }     
        
        private void ResizeLeftPanel()
        {
            if (pnlTheoryCard == null || rtbTheory == null) return;
            int cardW = splitContainerMain.Panel1.ClientSize.Width - 16 - 8; 
            int cardH = splitContainerMain.Panel1.ClientSize.Height - 12 - 12;
            pnlTheoryCard.Size = new Size(cardW, cardH);
            rtbTheory.Size = new Size(pnlTheoryCard.Width - 32, pnlTheoryCard.Height - 62);
        }

        private void ResizeRightPanel()
        {
            if (pnlEditorCard == null || pnlConsoleCard == null) return;

            int cardW = splitContainerMain.Panel2.ClientSize.Width - 8 - 16;

            
            pnlEditorCard.Width = cardW;
            fctbEditor.Size = new Size(pnlEditorCard.Width, pnlEditorCard.Height - 42);

            
            int consoleCardW = cardW;
            pnlConsoleCard.Width = consoleCardW;
            btnRunCode.Location = new Point(pnlConsoleCard.Width - btnRunCode.Width - 14, 10);

            
            int outputTop = 70;
            int outputH = Math.Max(60, pnlConsoleCard.Height - outputTop - 12);
            txtConsoleOutput.Location = new Point(0, outputTop);
            txtConsoleOutput.Size = new Size(pnlConsoleCard.Width, outputH);

            
            pnlConsoleCard.Invalidate();
        }

        private async void LoadLessonContent()
        {
            Services.LessonContent content = await DatabaseHelper.GetLessonContentAsync(_topicId);

            if (content == null)
            {
                lblLessonTitle.Text = "Материалы не найдены";
                rtbTheory.Text = "Для данной темы ещё не добавлены теоретические материалы или практические задания.";
                fctbEditor.Text = "";
                fctbEditor.Enabled = false;
                btnRunCode.Enabled = false;
                btnRunCode.BackColor = Color.FromArgb(160, 170, 185);
                return;
            }

            _actualLessonId = content.LessonId;
            _expectedOutput = content.ExpectedOutput;
            lblLessonTitle.Text = content.Title;
            rtbTheory.Text = content.Theory;

            string raw = string.IsNullOrEmpty(content.CodeTemplate)
                ? "// Напишите ваш код здесь...\r\n"
                : content.CodeTemplate
                    .Replace("\\n", "\r\n")
                    .Replace("/n", "\r\n")
                    .Replace("  //", " //");

            fctbEditor.Text = raw;
        }

        private void btnRunCode_Click(object sender, EventArgs e)
        {
            txtConsoleOutput.Clear();
            SetStatus("⏳  Компиляция...", Color.FromArgb(90, 100, 120));
            this.Refresh();

            bool isSuccess;
            string output = RoslynCompiler.CompileAndRun(fctbEditor.Text, out isSuccess);

            if (isSuccess)
            {
                txtConsoleOutput.SelectionColor = Color.FromArgb(180, 230, 180);
                txtConsoleOutput.AppendText(output);

                if (output.Trim() == _expectedOutput.Trim())
                {
                    SetStatus("✔  Задание выполнено!", Color.FromArgb(56, 180, 100));
                    if (_actualLessonId != Guid.Empty)
                        DatabaseHelper.SaveProgressAsync(_actualLessonId);
                }
                else
                {
                    SetStatus("✘  Неверный результат.", Color.FromArgb(220, 110, 50));
                    txtConsoleOutput.SelectionColor = Color.FromArgb(160, 160, 180);
                    txtConsoleOutput.AppendText($"\r\n\r\n[Ожидалось]: {_expectedOutput.Trim()}");
                    txtConsoleOutput.AppendText($"\r\n[Получено]:  {output.Trim()}");
                }
            }
            else
            {
                txtConsoleOutput.SelectionColor = Color.FromArgb(255, 110, 100);
                txtConsoleOutput.AppendText(output);
                SetStatus("✘  Ошибка компиляции.", Color.FromArgb(220, 60, 50));
            }
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        private void LessonForm_Load(object sender, EventArgs e) { }
        private void LessonForm_Load_1(object sender, EventArgs e) { }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            ResizeLeftPanel();
            ResizeRightPanel();
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}