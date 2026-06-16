using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpDesctop.Models;
using CSharpDesctop.Services;

namespace CSharpDesctop.Forms
{
    public partial class AdminForm : Form
    {
        
        private List<ChapterModel> _chapters = new();
        private List<TopicModel> _topics = new();
        private List<LessonModel> _lessons = new();
        private List<CodeBlockModel> _codeBlocks = new();

        private ChapterModel? _selChapter;
        private TopicModel? _selTopic;
        private LessonModel? _selLesson;
        private CodeBlockModel? _selCodeBlock;

        
        public AdminForm()
        {
            InitializeComponent();
            tabMain.DrawItem += TabMain_DrawItem;
        }

        
        
        
        private void TabMain_DrawItem(object? sender, DrawItemEventArgs e)
        {
            bool sel = e.Index == tabMain.SelectedIndex;
            e.Graphics.FillRectangle(
                new SolidBrush(sel ? Color.FromArgb(45, 125, 210) : Color.FromArgb(220, 228, 240)),
                e.Bounds);
            TextRenderer.DrawText(e.Graphics, tabMain.TabPages[e.Index].Text,
                new Font("Segoe UI", 10F, FontStyle.Bold), e.Bounds,
                sel ? Color.White : Color.FromArgb(30, 35, 45),
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        
        
        
        private async void AdminForm_Load(object sender, EventArgs e) =>
            await RefreshAllAsync();

        
        
        
        private async Task RefreshAllAsync()
        {
            _chapters = await DatabaseHelper.GetChaptersAsync() ?? new();
            _topics = await DatabaseHelper.GetAllTopicsAsync() ?? new();
            _lessons = await DatabaseHelper.GetAllLessonsAsync() ?? new();

            
            lstChapters.DataSource = null;
            lstChapters.DataSource = _chapters;
            lstChapters.DisplayMember = "Title";
            lstChapters.ValueMember = "Id";

            
            var chapSrc = new List<ChapterModel>(_chapters);
            chapSrc.Insert(0, new ChapterModel { Id = Guid.Empty, Title = "— выберите главу —" });
            cmbTopicChapter.DataSource = chapSrc;
            cmbTopicChapter.DisplayMember = "Title";
            cmbTopicChapter.ValueMember = "Id";

            
            var topSrc = new List<TopicModel>(_topics);
            topSrc.Insert(0, new TopicModel { Id = Guid.Empty, Title = "— выберите тему —" });
            cmbLessonTopic.DataSource = topSrc;
            cmbLessonTopic.DisplayMember = "Title";
            cmbLessonTopic.ValueMember = "Id";

            
            var lesSrc = new List<LessonModel>(_lessons);
            lesSrc.Insert(0, new LessonModel { Id = Guid.Empty, Title = "— выберите урок —" });
            cmbCodeLesson.DataSource = lesSrc;
            cmbCodeLesson.DisplayMember = "Title";
            cmbCodeLesson.ValueMember = "Id";

            ClearChapterFields();
        }

        private async Task TopicsLoadAsync()
        {
            if (cmbTopicChapter.SelectedItem is not ChapterModel ch || ch.Id == Guid.Empty)
            { lstTopics.DataSource = null; return; }

            _topics = await DatabaseHelper.GetTopicsByChapterAsync(ch.Id) ?? new();
            lstTopics.DataSource = null;
            lstTopics.DataSource = _topics;
            lstTopics.DisplayMember = "Title";
            lstTopics.ValueMember = "Id";
            ClearTopicFields();
        }

        private async Task LessonsLoadAsync()
        {
            if (cmbLessonTopic.SelectedItem is not TopicModel tp || tp.Id == Guid.Empty)
            { lstLessons.DataSource = null; return; }

            _lessons = await DatabaseHelper.GetLessonsByTopicAsync(tp.Id) ?? new();
            lstLessons.DataSource = null;
            lstLessons.DataSource = _lessons;
            lstLessons.DisplayMember = "Title";
            lstLessons.ValueMember = "Id";
            ClearLessonFields();
        }

        private async Task CodeBlocksLoadAsync()
        {
            if (cmbCodeLesson.SelectedItem is not LessonModel ls || ls.Id == Guid.Empty)
            { lstCodeBlocks.DataSource = null; return; }

            _codeBlocks = await DatabaseHelper.GetCodeBlocksByLessonAsync(ls.Id) ?? new();
            lstCodeBlocks.DataSource = null;
            lstCodeBlocks.DataSource = _codeBlocks;
            lstCodeBlocks.DisplayMember = "OrderIndex";
            lstCodeBlocks.ValueMember = "Id";
            ClearCodeFields();
        }

        
        
        
        private void lstChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selChapter = lstChapters.SelectedItem as ChapterModel;
            if (_selChapter == null) return;
            txtChTitle.Text = _selChapter.Title;
            txtChDesc.Text = _selChapter.Description;
            txtChCoverUrl.Text = _selChapter.CoverUrl;
            numChOrder.Value = _selChapter.OrderIndex;
        }

        private void lstTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selTopic = lstTopics.SelectedItem as TopicModel;
            if (_selTopic == null) return;
            txtTopTitle.Text = _selTopic.Title;
            txtTopDesc.Text = _selTopic.Description;
            numTopOrder.Value = _selTopic.OrderIndex;
        }

        private void lstLessons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selLesson = lstLessons.SelectedItem as LessonModel;
            if (_selLesson == null) return;
            txtLesTitle.Text = _selLesson.Title;
            txtLesContent.Text = _selLesson.ContentHtml;
            numLesOrder.Value = _selLesson.OrderIndex;
        }

        private void lstCodeBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selCodeBlock = lstCodeBlocks.SelectedItem as CodeBlockModel;
            if (_selCodeBlock == null) return;
            txtCode.Text = _selCodeBlock.Code;
            txtExpected.Text = _selCodeBlock.ExpectedOutput;
            txtLang.Text = _selCodeBlock.Language;
            numCodeOrder.Value = _selCodeBlock.OrderIndex;
        }

        
        private async void cmbTopicChapter_SelectedIndexChanged(object sender, EventArgs e) =>
            await TopicsLoadAsync();

        private async void cmbLessonTopic_SelectedIndexChanged(object sender, EventArgs e) =>
            await LessonsLoadAsync();

        private async void cmbCodeLesson_SelectedIndexChanged(object sender, EventArgs e) =>
            await CodeBlocksLoadAsync();

        
        
        

        
        private async void btnChAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChTitle.Text)) { Warn("Введите название главы."); return; }
            await DatabaseHelper.InsertChapterAsync(new ChapterModel
            {
                Id = Guid.NewGuid(),
                Title = txtChTitle.Text.Trim(),
                Description = txtChDesc.Text.Trim(),
                CoverUrl = txtChCoverUrl.Text.Trim(),
                OrderIndex = (short)numChOrder.Value
            });
            await RefreshAllAsync();
            Info("Глава добавлена.");
        }

        private async void btnChSave_Click(object sender, EventArgs e)
        {
            if (_selChapter == null) { Warn("Выберите главу в списке."); return; }
            _selChapter.Title = txtChTitle.Text.Trim();
            _selChapter.Description = txtChDesc.Text.Trim();
            _selChapter.CoverUrl = txtChCoverUrl.Text.Trim();
            _selChapter.OrderIndex = (short)numChOrder.Value;
            await DatabaseHelper.UpdateChapterAsync(_selChapter);
            await RefreshAllAsync();
            Info("Глава сохранена.");
        }

        private async void btnChDelete_Click(object sender, EventArgs e)
        {
            if (_selChapter == null) { Warn("Выберите главу в списке."); return; }
            if (!Confirm($"Удалить главу «{_selChapter.Title}»?\nВсе темы и уроки будут удалены (CASCADE).")) return;
            await DatabaseHelper.DeleteChapterAsync(_selChapter.Id);
            await RefreshAllAsync();
        }

        
        private async void btnTopAdd_Click(object sender, EventArgs e)
        {
            if (cmbTopicChapter.SelectedItem is not ChapterModel ch || ch.Id == Guid.Empty)
            { Warn("Выберите главу."); return; }
            if (string.IsNullOrWhiteSpace(txtTopTitle.Text)) { Warn("Введите название темы."); return; }
            await DatabaseHelper.InsertTopicAsync(new TopicModel
            {
                Id = Guid.NewGuid(),
                ChapterId = ch.Id,
                Title = txtTopTitle.Text.Trim(),
                Description = txtTopDesc.Text.Trim(),
                OrderIndex = (short)numTopOrder.Value
            });
            await TopicsLoadAsync();
            Info("Тема добавлена.");
        }

        private async void btnTopSave_Click(object sender, EventArgs e)
        {
            if (_selTopic == null) { Warn("Выберите тему в списке."); return; }
            _selTopic.Title = txtTopTitle.Text.Trim();
            _selTopic.Description = txtTopDesc.Text.Trim();
            _selTopic.OrderIndex = (short)numTopOrder.Value;
            await DatabaseHelper.UpdateTopicAsync(_selTopic);
            await TopicsLoadAsync();
            Info("Тема сохранена.");
        }

        private async void btnTopDelete_Click(object sender, EventArgs e)
        {
            if (_selTopic == null) { Warn("Выберите тему в списке."); return; }
            if (!Confirm($"Удалить тему «{_selTopic.Title}»?\nВсе уроки внутри будут удалены.")) return;
            await DatabaseHelper.DeleteTopicAsync(_selTopic.Id);
            await TopicsLoadAsync();
        }

        
        private async void btnLesAdd_Click(object sender, EventArgs e)
        {
            if (cmbLessonTopic.SelectedItem is not TopicModel tp || tp.Id == Guid.Empty)
            { Warn("Выберите тему."); return; }
            if (string.IsNullOrWhiteSpace(txtLesTitle.Text)) { Warn("Введите название урока."); return; }
            await DatabaseHelper.InsertLessonAsync(new LessonModel
            {
                Id = Guid.NewGuid(),
                TopicId = tp.Id,
                Title = txtLesTitle.Text.Trim(),
                ContentHtml = txtLesContent.Text,
                OrderIndex = (short)numLesOrder.Value
            });
            await LessonsLoadAsync();
            Info("Урок добавлен.");
        }

        private async void btnLesSave_Click(object sender, EventArgs e)
        {
            if (_selLesson == null) { Warn("Выберите урок в списке."); return; }
            _selLesson.Title = txtLesTitle.Text.Trim();
            _selLesson.ContentHtml = txtLesContent.Text;
            _selLesson.OrderIndex = (short)numLesOrder.Value;
            await DatabaseHelper.UpdateLessonAsync(_selLesson);
            await LessonsLoadAsync();
            Info("Урок сохранён.");
        }

        private async void btnLesDelete_Click(object sender, EventArgs e)
        {
            if (_selLesson == null) { Warn("Выберите урок в списке."); return; }
            if (!Confirm($"Удалить урок «{_selLesson.Title}»?")) return;
            await DatabaseHelper.DeleteLessonAsync(_selLesson.Id);
            await LessonsLoadAsync();
        }

        
        private async void btnCodeAdd_Click(object sender, EventArgs e)
        {
            if (cmbCodeLesson.SelectedItem is not LessonModel ls || ls.Id == Guid.Empty)
            { Warn("Выберите урок."); return; }
            if (string.IsNullOrWhiteSpace(txtCode.Text)) { Warn("Введите код задания."); return; }
            await DatabaseHelper.InsertCodeBlockAsync(new CodeBlockModel
            {
                Id = Guid.NewGuid(),
                LessonId = ls.Id,
                Code = txtCode.Text,
                ExpectedOutput = txtExpected.Text,
                Language = string.IsNullOrWhiteSpace(txtLang.Text) ? "csharp" : txtLang.Text.Trim(),
                OrderIndex = (short)numCodeOrder.Value
            });
            await CodeBlocksLoadAsync();
            Info("Задание добавлено.");
        }

        private async void btnCodeSave_Click(object sender, EventArgs e)
        {
            if (_selCodeBlock == null) { Warn("Выберите задание в списке."); return; }
            _selCodeBlock.Code = txtCode.Text;
            _selCodeBlock.ExpectedOutput = txtExpected.Text;
            _selCodeBlock.Language = txtLang.Text.Trim();
            _selCodeBlock.OrderIndex = (short)numCodeOrder.Value;
            await DatabaseHelper.UpdateCodeBlockAsync(_selCodeBlock);
            await CodeBlocksLoadAsync();
            Info("Задание сохранено.");
        }

        private async void btnCodeDelete_Click(object sender, EventArgs e)
        {
            if (_selCodeBlock == null) { Warn("Выберите задание в списке."); return; }
            if (!Confirm("Удалить это задание?")) return;
            await DatabaseHelper.DeleteCodeBlockAsync(_selCodeBlock.Id);
            await CodeBlocksLoadAsync();
        }

        
        
        
        private void ClearChapterFields()
        {
            txtChTitle.Clear(); txtChDesc.Clear(); txtChCoverUrl.Clear();
            numChOrder.Value = 0; _selChapter = null;
        }
        private void ClearTopicFields()
        {
            txtTopTitle.Clear(); txtTopDesc.Clear();
            numTopOrder.Value = 0; _selTopic = null;
        }
        private void ClearLessonFields()
        {
            txtLesTitle.Clear(); txtLesContent.Clear();
            numLesOrder.Value = 0; _selLesson = null;
        }
        private void ClearCodeFields()
        {
            txtCode.Clear(); txtExpected.Clear();
            txtLang.Text = "csharp"; numCodeOrder.Value = 0; _selCodeBlock = null;
        }

        
        
        
        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            var main = Application.OpenForms["MainForm"];
            if (main != null)
            {
                main.Show();
                main.BringToFront();
            }
            else
            {
                new MainForm().Show();
            }
            this.Close();
        }

        
        
        
        private static void Warn(string msg) =>
            MessageBox.Show(msg, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private static void Info(string msg) =>
            MessageBox.Show(msg, "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private static bool Confirm(string msg) =>
            MessageBox.Show(msg, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }


    //mavv grkv adlq qdpi


    internal class AdminTopBar : Panel
    {
        public AdminTopBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height),
                Color.FromArgb(180, 30, 30),
                Color.FromArgb(229, 57, 53),
                LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);
            using var pen = new Pen(Color.FromArgb(20, 0, 0, 60), 1);
            e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);
            base.OnPaint(e);
        }
    }
}