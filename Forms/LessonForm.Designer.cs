using CSharpDesctop.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    partial class LessonForm
    {
        private System.ComponentModel.IContainer components = null;

        // Top bar (gradient, like MainForm)
        private LessonTopBar pnlTopBar;
        private CSharpDesctop.Controls.RoundedButton btnBack;
        private System.Windows.Forms.Label lblLessonTitle;

        // Main layout
        private System.Windows.Forms.SplitContainer splitContainerMain;

        // Left panel – theory card
        private CSharpDesctop.Controls.RoundedPanel pnlTheoryCard;
        private System.Windows.Forms.Label lblTheoryHeader;
        private System.Windows.Forms.RichTextBox rtbTheory;

        // Right panel – editor card + console card
        private CSharpDesctop.Controls.RoundedPanel pnlEditorCard;
        private System.Windows.Forms.Label lblEditorHeader;
        private FastColoredTextBoxNS.FastColoredTextBox fctbEditor;

        private CSharpDesctop.Controls.RoundedPanel pnlConsoleCard;
        private System.Windows.Forms.Label lblConsoleHeader;
        private System.Windows.Forms.Label lblStatus;
        private CSharpDesctop.Controls.RoundedButton btnRunCode;
        private System.Windows.Forms.RichTextBox txtConsoleOutput;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LessonForm));
            pnlTopBar = new LessonTopBar();
            btnBack = new RoundedButton();
            lblLessonTitle = new Label();
            splitContainerMain = new SplitContainer();
            pnlTheoryCard = new RoundedPanel();
            rtbTheory = new RichTextBox();
            lblTheoryHeader = new Label();
            pnlConsoleCard = new RoundedPanel();
            txtConsoleOutput = new RichTextBox();
            lblStatus = new Label();
            btnRunCode = new RoundedButton();
            lblConsoleHeader = new Label();
            pnlEditorCard = new RoundedPanel();
            fctbEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            lblEditorHeader = new Label();
            pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlTheoryCard.SuspendLayout();
            pnlConsoleCard.SuspendLayout();
            pnlEditorCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fctbEditor).BeginInit();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.Controls.Add(btnBack);
            pnlTopBar.Controls.Add(lblLessonTitle);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Padding = new Padding(12, 0, 16, 0);
            pnlTopBar.Size = new Size(1280, 60);
            pnlTopBar.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(192, 192, 255);
            btnBack.CornerRadius = 8;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderColor = Color.FromArgb(60, 255, 255, 255);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(12, 13);
            btnBack.Name = "btnBack";
            btnBack.ShowShadow = false;
            btnBack.Size = new Size(96, 34);
            btnBack.TabIndex = 0;
            btnBack.Text = "← Назад";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblLessonTitle
            // 
            lblLessonTitle.BackColor = Color.Transparent;
            lblLessonTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLessonTitle.ForeColor = Color.White;
            lblLessonTitle.Location = new Point(124, 14);
            lblLessonTitle.Name = "lblLessonTitle";
            lblLessonTitle.Size = new Size(900, 32);
            lblLessonTitle.TabIndex = 1;
            lblLessonTitle.Text = "Урок";
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.FromArgb(240, 244, 249);
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 60);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.FromArgb(240, 244, 249);
            splitContainerMain.Panel1.Controls.Add(pnlTheoryCard);
            splitContainerMain.Panel1.Padding = new Padding(16, 12, 8, 12);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.FromArgb(240, 244, 249);
            splitContainerMain.Panel2.Controls.Add(pnlConsoleCard);
            splitContainerMain.Panel2.Controls.Add(pnlEditorCard);
            splitContainerMain.Panel2.Padding = new Padding(8, 12, 16, 12);
            splitContainerMain.Size = new Size(1280, 660);
            splitContainerMain.SplitterDistance = 867;
            splitContainerMain.SplitterWidth = 8;
            splitContainerMain.TabIndex = 0;
            splitContainerMain.SplitterMoved += splitContainerMain_SplitterMoved;
            // 
            // pnlTheoryCard
            // 
            pnlTheoryCard.BackColor = Color.White;
            pnlTheoryCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlTheoryCard.BorderThickness = 1;
            pnlTheoryCard.Controls.Add(rtbTheory);
            pnlTheoryCard.Controls.Add(lblTheoryHeader);
            pnlTheoryCard.CornerRadius = 16;
            pnlTheoryCard.Dock = DockStyle.Fill;
            pnlTheoryCard.HoverHighlight = false;
            pnlTheoryCard.Location = new Point(16, 12);
            pnlTheoryCard.Name = "pnlTheoryCard";
            pnlTheoryCard.Size = new Size(843, 636);
            pnlTheoryCard.TabIndex = 0;
            // 
            // rtbTheory
            // 
            rtbTheory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbTheory.BackColor = Color.White;
            rtbTheory.BorderStyle = BorderStyle.None;
            rtbTheory.Font = new Font("Segoe UI", 11F);
            rtbTheory.ForeColor = Color.FromArgb(40, 50, 70);
            rtbTheory.Location = new Point(16, 46);
            rtbTheory.Name = "rtbTheory";
            rtbTheory.ReadOnly = true;
            rtbTheory.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbTheory.Size = new Size(746, 632);
            rtbTheory.TabIndex = 0;
            rtbTheory.Text = "";
            // 
            // lblTheoryHeader
            // 
            lblTheoryHeader.AutoSize = true;
            lblTheoryHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTheoryHeader.ForeColor = Color.FromArgb(25, 35, 55);
            lblTheoryHeader.Location = new Point(16, 14);
            lblTheoryHeader.Name = "lblTheoryHeader";
            lblTheoryHeader.Size = new Size(91, 20);
            lblTheoryHeader.TabIndex = 1;
            lblTheoryHeader.Text = "📖  Теория";
            // 
            // pnlConsoleCard
            // 
            pnlConsoleCard.BackColor = Color.White;
            pnlConsoleCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlConsoleCard.BorderThickness = 1;
            pnlConsoleCard.Controls.Add(txtConsoleOutput);
            pnlConsoleCard.Controls.Add(lblStatus);
            pnlConsoleCard.Controls.Add(btnRunCode);
            pnlConsoleCard.Controls.Add(lblConsoleHeader);
            pnlConsoleCard.CornerRadius = 16;
            pnlConsoleCard.Dock = DockStyle.Fill;
            pnlConsoleCard.HoverHighlight = false;
            pnlConsoleCard.Location = new Point(8, 382);
            pnlConsoleCard.Name = "pnlConsoleCard";
            pnlConsoleCard.Size = new Size(381, 266);
            pnlConsoleCard.TabIndex = 0;
            // 
            // txtConsoleOutput
            // 
            txtConsoleOutput.BackColor = Color.FromArgb(18, 20, 28);
            txtConsoleOutput.BorderStyle = BorderStyle.None;
            txtConsoleOutput.Dock = DockStyle.Bottom;
            txtConsoleOutput.Font = new Font("Consolas", 11F);
            txtConsoleOutput.ForeColor = Color.FromArgb(180, 230, 180);
            txtConsoleOutput.Location = new Point(0, 170);
            txtConsoleOutput.Name = "txtConsoleOutput";
            txtConsoleOutput.ReadOnly = true;
            txtConsoleOutput.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtConsoleOutput.Size = new Size(381, 96);
            txtConsoleOutput.TabIndex = 0;
            txtConsoleOutput.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(100, 110, 130);
            lblStatus.Location = new Point(16, 50);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(125, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ожидание запуска...";
            // 
            // btnRunCode
            // 
            btnRunCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRunCode.BackColor = Color.FromArgb(45, 125, 210);
            btnRunCode.CornerRadius = 8;
            btnRunCode.Cursor = Cursors.Hand;
            btnRunCode.FlatStyle = FlatStyle.Flat;
            btnRunCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRunCode.ForeColor = Color.White;
            btnRunCode.Location = new Point(248, 5);
            btnRunCode.Name = "btnRunCode";
            btnRunCode.ShowShadow = true;
            btnRunCode.Size = new Size(130, 34);
            btnRunCode.TabIndex = 2;
            btnRunCode.Text = "▶  Запустить";
            btnRunCode.UseVisualStyleBackColor = false;
            btnRunCode.Click += btnRunCode_Click;
            // 
            // lblConsoleHeader
            // 
            lblConsoleHeader.AutoSize = true;
            lblConsoleHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblConsoleHeader.ForeColor = Color.FromArgb(25, 35, 55);
            lblConsoleHeader.Location = new Point(16, 12);
            lblConsoleHeader.Name = "lblConsoleHeader";
            lblConsoleHeader.Size = new Size(159, 20);
            lblConsoleHeader.TabIndex = 3;
            lblConsoleHeader.Text = "⚡  Консоль вывода";
            // 
            // pnlEditorCard
            // 
            pnlEditorCard.BackColor = Color.White;
            pnlEditorCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlEditorCard.BorderThickness = 1;
            pnlEditorCard.Controls.Add(fctbEditor);
            pnlEditorCard.Controls.Add(lblEditorHeader);
            pnlEditorCard.CornerRadius = 16;
            pnlEditorCard.Dock = DockStyle.Top;
            pnlEditorCard.HoverHighlight = false;
            pnlEditorCard.Location = new Point(8, 12);
            pnlEditorCard.Name = "pnlEditorCard";
            pnlEditorCard.Size = new Size(381, 370);
            pnlEditorCard.TabIndex = 1;
            // 
            // fctbEditor
            // 
            fctbEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fctbEditor.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            fctbEditor.AutoScrollMinSize = new Size(35, 21);
            fctbEditor.BackBrush = null;
            fctbEditor.CharHeight = 17;
            fctbEditor.CharWidth = 8;
            fctbEditor.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fctbEditor.Font = new Font("Consolas", 11F);
            //fctbEditor.Hotkeys = resources.GetString("fctbEditor.Hotkeys");
            fctbEditor.IsReplaceMode = false;
            fctbEditor.Location = new Point(3, 42);
            fctbEditor.Name = "fctbEditor";
            fctbEditor.Paddings = new Padding(8, 4, 0, 0);
            fctbEditor.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fctbEditor.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fctbEditor.ServiceColors");
            fctbEditor.Size = new Size(375, 328);
            fctbEditor.TabIndex = 0;
            fctbEditor.Zoom = 100;
            // 
            // lblEditorHeader
            // 
            lblEditorHeader.AutoSize = true;
            lblEditorHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEditorHeader.ForeColor = Color.FromArgb(25, 35, 55);
            lblEditorHeader.Location = new Point(16, 12);
            lblEditorHeader.Name = "lblEditorHeader";
            lblEditorHeader.Size = new Size(144, 20);
            lblEditorHeader.TabIndex = 1;
            lblEditorHeader.Text = "💻  Редактор кода";
            // 
            // LessonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 244, 249);
            ClientSize = new Size(1280, 720);
            Controls.Add(splitContainerMain);
            Controls.Add(pnlTopBar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LessonForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# Manual — Урок";
            Load += LessonForm_Load_1;
            pnlTopBar.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlTheoryCard.ResumeLayout(false);
            pnlTheoryCard.PerformLayout();
            pnlConsoleCard.ResumeLayout(false);
            pnlConsoleCard.PerformLayout();
            pnlEditorCard.ResumeLayout(false);
            pnlEditorCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fctbEditor).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }

    // ── Gradient top-bar for LessonForm (mirrors MainForm's GradientTopBar) ──
    internal class LessonTopBar : Panel
    {
        public LessonTopBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            using (var brush = new LinearGradientBrush(rect,
                Color.FromArgb(22, 72, 162),
                Color.FromArgb(50, 122, 220),
                LinearGradientMode.Horizontal))
                e.Graphics.FillRectangle(brush, rect);

            // Subtle bottom border
            using (var pen = new Pen(Color.FromArgb(25, 0, 0, 60), 1))
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            base.OnPaint(e);
        }
    }
}