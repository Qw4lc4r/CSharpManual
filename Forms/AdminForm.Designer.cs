namespace CSharpDesctop.Forms
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            
            this.pnlTopBar = new AdminTopBar();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.btnBackToMain = new System.Windows.Forms.Button();

            
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabChapters = new System.Windows.Forms.TabPage();
            this.tabTopics = new System.Windows.Forms.TabPage();
            this.tabLessons = new System.Windows.Forms.TabPage();
            this.tabCodeBlocks = new System.Windows.Forms.TabPage();

            
            this.pnlChapterList = new System.Windows.Forms.Panel();
            this.lblChapterList = new System.Windows.Forms.Label();
            this.lstChapters = new System.Windows.Forms.ListBox();
            this.pnlChapterForm = new System.Windows.Forms.Panel();
            this.lblChTitle = new System.Windows.Forms.Label();
            this.txtChTitle = new System.Windows.Forms.TextBox();
            this.lblChDesc = new System.Windows.Forms.Label();
            this.txtChDesc = new System.Windows.Forms.TextBox();
            this.lblChCoverUrl = new System.Windows.Forms.Label();
            this.txtChCoverUrl = new System.Windows.Forms.TextBox();
            this.lblChOrder = new System.Windows.Forms.Label();
            this.numChOrder = new System.Windows.Forms.NumericUpDown();
            this.pnlChapterBtns = new System.Windows.Forms.Panel();
            this.btnChAdd = new System.Windows.Forms.Button();
            this.btnChSave = new System.Windows.Forms.Button();
            this.btnChDelete = new System.Windows.Forms.Button();

            
            this.pnlTopicFilter = new System.Windows.Forms.Panel();
            this.lblTopicChFilter = new System.Windows.Forms.Label();
            this.cmbTopicChapter = new System.Windows.Forms.ComboBox();
            this.pnlTopicList = new System.Windows.Forms.Panel();
            this.lblTopicList = new System.Windows.Forms.Label();
            this.lstTopics = new System.Windows.Forms.ListBox();
            this.pnlTopicForm = new System.Windows.Forms.Panel();
            this.lblTopTitle = new System.Windows.Forms.Label();
            this.txtTopTitle = new System.Windows.Forms.TextBox();
            this.lblTopDesc = new System.Windows.Forms.Label();
            this.txtTopDesc = new System.Windows.Forms.TextBox();
            this.lblTopOrder = new System.Windows.Forms.Label();
            this.numTopOrder = new System.Windows.Forms.NumericUpDown();
            this.pnlTopicBtns = new System.Windows.Forms.Panel();
            this.btnTopAdd = new System.Windows.Forms.Button();
            this.btnTopSave = new System.Windows.Forms.Button();
            this.btnTopDelete = new System.Windows.Forms.Button();

            
            this.pnlLessonFilter = new System.Windows.Forms.Panel();
            this.lblLessonTopFilter = new System.Windows.Forms.Label();
            this.cmbLessonTopic = new System.Windows.Forms.ComboBox();
            this.pnlLessonList = new System.Windows.Forms.Panel();
            this.lblLessonList = new System.Windows.Forms.Label();
            this.lstLessons = new System.Windows.Forms.ListBox();
            this.pnlLessonForm = new System.Windows.Forms.Panel();
            this.lblLesTitle = new System.Windows.Forms.Label();
            this.txtLesTitle = new System.Windows.Forms.TextBox();
            this.lblLesContent = new System.Windows.Forms.Label();
            this.txtLesContent = new System.Windows.Forms.TextBox();
            this.lblLesOrder = new System.Windows.Forms.Label();
            this.numLesOrder = new System.Windows.Forms.NumericUpDown();
            this.pnlLessonBtns = new System.Windows.Forms.Panel();
            this.btnLesAdd = new System.Windows.Forms.Button();
            this.btnLesSave = new System.Windows.Forms.Button();
            this.btnLesDelete = new System.Windows.Forms.Button();

            
            this.pnlCodeFilter = new System.Windows.Forms.Panel();
            this.lblCodeLesFilter = new System.Windows.Forms.Label();
            this.cmbCodeLesson = new System.Windows.Forms.ComboBox();
            this.pnlCodeList = new System.Windows.Forms.Panel();
            this.lblCodeList = new System.Windows.Forms.Label();
            this.lstCodeBlocks = new System.Windows.Forms.ListBox();
            this.pnlCodeForm = new System.Windows.Forms.Panel();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblExpected = new System.Windows.Forms.Label();
            this.txtExpected = new System.Windows.Forms.TextBox();
            this.lblLang = new System.Windows.Forms.Label();
            this.txtLang = new System.Windows.Forms.TextBox();
            this.lblCodeOrder = new System.Windows.Forms.Label();
            this.numCodeOrder = new System.Windows.Forms.NumericUpDown();
            this.pnlCodeBtns = new System.Windows.Forms.Panel();
            this.btnCodeAdd = new System.Windows.Forms.Button();
            this.btnCodeSave = new System.Windows.Forms.Button();
            this.btnCodeDelete = new System.Windows.Forms.Button();

            
            
            
            this.pnlTopBar.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabChapters.SuspendLayout();
            this.tabTopics.SuspendLayout();
            this.tabLessons.SuspendLayout();
            this.tabCodeBlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.numChOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numTopOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numLesOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numCodeOrder).BeginInit();
            this.SuspendLayout();

            
            
            
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 60;
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Controls.Add(this.lblAdminTitle);
            this.pnlTopBar.Controls.Add(this.btnBackToMain);

            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.White;
            this.lblAdminTitle.Location = new System.Drawing.Point(16, 14);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Text = "🛠  Панель администратора";

            this.btnBackToMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBackToMain.Name = "btnBackToMain";
            this.btnBackToMain.Text = "← На главную";
            this.btnBackToMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBackToMain.ForeColor = System.Drawing.Color.White;
            this.btnBackToMain.BackColor = System.Drawing.Color.FromArgb(60, 0, 0, 0);
            this.btnBackToMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToMain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(120, 255, 255, 255);
            this.btnBackToMain.FlatAppearance.BorderSize = 1;
            this.btnBackToMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 255, 255, 255);
            this.btnBackToMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 255, 255, 255);
            this.btnBackToMain.Size = new System.Drawing.Size(130, 36);
            this.btnBackToMain.Location = new System.Drawing.Point(954, 12);
            this.btnBackToMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToMain.Click += new System.EventHandler(this.btnBackToMain_Click);

            
            
            
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabMain.ItemSize = new System.Drawing.Size(150, 36);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.Name = "tabMain";
            this.tabMain.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabChapters, this.tabTopics, this.tabLessons, this.tabCodeBlocks });
            this.tabMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabMain_DrawItem);

            
            
            
            this.tabChapters.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.tabChapters.Name = "tabChapters";
            this.tabChapters.Text = "📚 Главы";
            this.tabChapters.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlChapterList, this.pnlChapterForm, this.pnlChapterBtns });

            
            this.pnlChapterList.Location = new System.Drawing.Point(10, 10);
            this.pnlChapterList.Size = new System.Drawing.Size(280, 470);
            this.pnlChapterList.BackColor = System.Drawing.Color.Transparent;
            this.pnlChapterList.Name = "pnlChapterList";
            this.pnlChapterList.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblChapterList, this.lstChapters });

            this.lblChapterList.AutoSize = true;
            this.lblChapterList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChapterList.ForeColor = System.Drawing.Color.FromArgb(45, 125, 210);
            this.lblChapterList.Location = new System.Drawing.Point(0, 0);
            this.lblChapterList.Name = "lblChapterList";
            this.lblChapterList.Text = "Главы";

            this.lstChapters.Location = new System.Drawing.Point(0, 24);
            this.lstChapters.Size = new System.Drawing.Size(280, 440);
            this.lstChapters.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstChapters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstChapters.BackColor = System.Drawing.Color.White;
            this.lstChapters.ItemHeight = 26;
            this.lstChapters.Name = "lstChapters";
            this.lstChapters.SelectedIndexChanged += new System.EventHandler(this.lstChapters_SelectedIndexChanged);

            
            this.pnlChapterForm.Location = new System.Drawing.Point(310, 10);
            this.pnlChapterForm.Size = new System.Drawing.Size(760, 300);
            this.pnlChapterForm.BackColor = System.Drawing.Color.White;
            this.pnlChapterForm.Name = "pnlChapterForm";
            this.pnlChapterForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblChTitle, this.txtChTitle,
                this.lblChDesc, this.txtChDesc,
                this.lblChCoverUrl, this.txtChCoverUrl,
                this.lblChOrder, this.numChOrder });

            
            SetupLabel(this.lblChTitle, "Название", new System.Drawing.Point(14, 12));
            SetupTextBox(this.txtChTitle, "txtChTitle", new System.Drawing.Point(14, 32), new System.Drawing.Size(730, 26));

            SetupLabel(this.lblChDesc, "Описание", new System.Drawing.Point(14, 66));
            SetupTextBoxML(this.txtChDesc, "txtChDesc", new System.Drawing.Point(14, 86), new System.Drawing.Size(730, 60));

            SetupLabel(this.lblChCoverUrl, "URL обложки", new System.Drawing.Point(14, 154));
            SetupTextBox(this.txtChCoverUrl, "txtChCoverUrl", new System.Drawing.Point(14, 174), new System.Drawing.Size(730, 26));

            SetupLabel(this.lblChOrder, "Порядок (#)", new System.Drawing.Point(14, 208));
            SetupNumeric(this.numChOrder, "numChOrder", new System.Drawing.Point(14, 228));

            
            this.pnlChapterBtns.Location = new System.Drawing.Point(310, 318);
            this.pnlChapterBtns.Size = new System.Drawing.Size(500, 40);
            this.pnlChapterBtns.BackColor = System.Drawing.Color.Transparent;
            this.pnlChapterBtns.Name = "pnlChapterBtns";
            this.pnlChapterBtns.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnChAdd, this.btnChSave, this.btnChDelete });

            SetupButton(this.btnChAdd, "btnChAdd", "＋ Добавить", System.Drawing.Color.FromArgb(67, 160, 71), new System.Drawing.Point(0, 0));
            SetupButton(this.btnChSave, "btnChSave", "💾 Сохранить", System.Drawing.Color.FromArgb(45, 125, 210), new System.Drawing.Point(148, 0));
            SetupButton(this.btnChDelete, "btnChDelete", "🗑 Удалить", System.Drawing.Color.FromArgb(229, 57, 53), new System.Drawing.Point(296, 0));

            this.btnChAdd.Click += new System.EventHandler(this.btnChAdd_Click);
            this.btnChSave.Click += new System.EventHandler(this.btnChSave_Click);
            this.btnChDelete.Click += new System.EventHandler(this.btnChDelete_Click);

            
            
            
            this.tabTopics.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.tabTopics.Name = "tabTopics";
            this.tabTopics.Text = "📑 Темы";
            this.tabTopics.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlTopicFilter, this.pnlTopicList, this.pnlTopicForm, this.pnlTopicBtns });

            
            this.pnlTopicFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlTopicFilter.Size = new System.Drawing.Size(600, 36);
            this.pnlTopicFilter.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopicFilter.Name = "pnlTopicFilter";
            this.pnlTopicFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTopicChFilter, this.cmbTopicChapter });

            SetupFilterLabel(this.lblTopicChFilter, "Глава:", new System.Drawing.Point(0, 8));
            SetupCombo(this.cmbTopicChapter, "cmbTopicChapter", new System.Drawing.Point(56, 4));
            this.cmbTopicChapter.SelectedIndexChanged += new System.EventHandler(this.cmbTopicChapter_SelectedIndexChanged);

            
            this.pnlTopicList.Location = new System.Drawing.Point(10, 54);
            this.pnlTopicList.Size = new System.Drawing.Size(280, 426);
            this.pnlTopicList.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopicList.Name = "pnlTopicList";
            this.pnlTopicList.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTopicList, this.lstTopics });

            SetupListHeader(this.lblTopicList, "Темы");
            SetupListBox(this.lstTopics, "lstTopics");
            this.lstTopics.SelectedIndexChanged += new System.EventHandler(this.lstTopics_SelectedIndexChanged);

            
            this.pnlTopicForm.Location = new System.Drawing.Point(310, 54);
            this.pnlTopicForm.Size = new System.Drawing.Size(760, 260);
            this.pnlTopicForm.BackColor = System.Drawing.Color.White;
            this.pnlTopicForm.Name = "pnlTopicForm";
            this.pnlTopicForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTopTitle, this.txtTopTitle,
                this.lblTopDesc, this.txtTopDesc,
                this.lblTopOrder, this.numTopOrder });

            SetupLabel(this.lblTopTitle, "Название", new System.Drawing.Point(14, 12));
            SetupTextBox(this.txtTopTitle, "txtTopTitle", new System.Drawing.Point(14, 32), new System.Drawing.Size(730, 26));
            SetupLabel(this.lblTopDesc, "Описание", new System.Drawing.Point(14, 66));
            SetupTextBoxML(this.txtTopDesc, "txtTopDesc", new System.Drawing.Point(14, 86), new System.Drawing.Size(730, 60));
            SetupLabel(this.lblTopOrder, "Порядок (#)", new System.Drawing.Point(14, 154));
            SetupNumeric(this.numTopOrder, "numTopOrder", new System.Drawing.Point(14, 174));

            
            this.pnlTopicBtns.Location = new System.Drawing.Point(310, 322);
            this.pnlTopicBtns.Size = new System.Drawing.Size(500, 40);
            this.pnlTopicBtns.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopicBtns.Name = "pnlTopicBtns";
            this.pnlTopicBtns.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnTopAdd, this.btnTopSave, this.btnTopDelete });

            SetupButton(this.btnTopAdd, "btnTopAdd", "＋ Добавить", System.Drawing.Color.FromArgb(67, 160, 71), new System.Drawing.Point(0, 0));
            SetupButton(this.btnTopSave, "btnTopSave", "💾 Сохранить", System.Drawing.Color.FromArgb(45, 125, 210), new System.Drawing.Point(148, 0));
            SetupButton(this.btnTopDelete, "btnTopDelete", "🗑 Удалить", System.Drawing.Color.FromArgb(229, 57, 53), new System.Drawing.Point(296, 0));

            this.btnTopAdd.Click += new System.EventHandler(this.btnTopAdd_Click);
            this.btnTopSave.Click += new System.EventHandler(this.btnTopSave_Click);
            this.btnTopDelete.Click += new System.EventHandler(this.btnTopDelete_Click);

            
            
            
            this.tabLessons.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.tabLessons.Name = "tabLessons";
            this.tabLessons.Text = "📝 Уроки";
            this.tabLessons.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlLessonFilter, this.pnlLessonList, this.pnlLessonForm, this.pnlLessonBtns });

            this.pnlLessonFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlLessonFilter.Size = new System.Drawing.Size(600, 36);
            this.pnlLessonFilter.BackColor = System.Drawing.Color.Transparent;
            this.pnlLessonFilter.Name = "pnlLessonFilter";
            this.pnlLessonFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLessonTopFilter, this.cmbLessonTopic });

            SetupFilterLabel(this.lblLessonTopFilter, "Тема:", new System.Drawing.Point(0, 8));
            SetupCombo(this.cmbLessonTopic, "cmbLessonTopic", new System.Drawing.Point(50, 4));
            this.cmbLessonTopic.SelectedIndexChanged += new System.EventHandler(this.cmbLessonTopic_SelectedIndexChanged);

            this.pnlLessonList.Location = new System.Drawing.Point(10, 54);
            this.pnlLessonList.Size = new System.Drawing.Size(280, 426);
            this.pnlLessonList.BackColor = System.Drawing.Color.Transparent;
            this.pnlLessonList.Name = "pnlLessonList";
            this.pnlLessonList.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLessonList, this.lstLessons });

            SetupListHeader(this.lblLessonList, "Уроки");
            SetupListBox(this.lstLessons, "lstLessons");
            this.lstLessons.SelectedIndexChanged += new System.EventHandler(this.lstLessons_SelectedIndexChanged);

            this.pnlLessonForm.Location = new System.Drawing.Point(310, 54);
            this.pnlLessonForm.Size = new System.Drawing.Size(760, 330);
            this.pnlLessonForm.BackColor = System.Drawing.Color.White;
            this.pnlLessonForm.Name = "pnlLessonForm";
            this.pnlLessonForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLesTitle, this.txtLesTitle,
                this.lblLesContent, this.txtLesContent,
                this.lblLesOrder, this.numLesOrder });

            SetupLabel(this.lblLesTitle, "Название", new System.Drawing.Point(14, 12));
            SetupTextBox(this.txtLesTitle, "txtLesTitle", new System.Drawing.Point(14, 32), new System.Drawing.Size(730, 26));
            SetupLabel(this.lblLesContent, "HTML-контент", new System.Drawing.Point(14, 66));
            SetupTextBoxML(this.txtLesContent, "txtLesContent", new System.Drawing.Point(14, 86), new System.Drawing.Size(730, 130));
            this.txtLesContent.Font = new System.Drawing.Font("Consolas", 9F);
            SetupLabel(this.lblLesOrder, "Порядок (#)", new System.Drawing.Point(14, 224));
            SetupNumeric(this.numLesOrder, "numLesOrder", new System.Drawing.Point(14, 244));

            this.pnlLessonBtns.Location = new System.Drawing.Point(310, 392);
            this.pnlLessonBtns.Size = new System.Drawing.Size(500, 40);
            this.pnlLessonBtns.BackColor = System.Drawing.Color.Transparent;
            this.pnlLessonBtns.Name = "pnlLessonBtns";
            this.pnlLessonBtns.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnLesAdd, this.btnLesSave, this.btnLesDelete });

            SetupButton(this.btnLesAdd, "btnLesAdd", "＋ Добавить", System.Drawing.Color.FromArgb(67, 160, 71), new System.Drawing.Point(0, 0));
            SetupButton(this.btnLesSave, "btnLesSave", "💾 Сохранить", System.Drawing.Color.FromArgb(45, 125, 210), new System.Drawing.Point(148, 0));
            SetupButton(this.btnLesDelete, "btnLesDelete", "🗑 Удалить", System.Drawing.Color.FromArgb(229, 57, 53), new System.Drawing.Point(296, 0));

            this.btnLesAdd.Click += new System.EventHandler(this.btnLesAdd_Click);
            this.btnLesSave.Click += new System.EventHandler(this.btnLesSave_Click);
            this.btnLesDelete.Click += new System.EventHandler(this.btnLesDelete_Click);

            
            
            
            this.tabCodeBlocks.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.tabCodeBlocks.Name = "tabCodeBlocks";
            this.tabCodeBlocks.Text = "💻 Задания";
            this.tabCodeBlocks.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlCodeFilter, this.pnlCodeList, this.pnlCodeForm, this.pnlCodeBtns });

            this.pnlCodeFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlCodeFilter.Size = new System.Drawing.Size(600, 36);
            this.pnlCodeFilter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCodeFilter.Name = "pnlCodeFilter";
            this.pnlCodeFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCodeLesFilter, this.cmbCodeLesson });

            SetupFilterLabel(this.lblCodeLesFilter, "Урок:", new System.Drawing.Point(0, 8));
            SetupCombo(this.cmbCodeLesson, "cmbCodeLesson", new System.Drawing.Point(52, 4));
            this.cmbCodeLesson.SelectedIndexChanged += new System.EventHandler(this.cmbCodeLesson_SelectedIndexChanged);

            this.pnlCodeList.Location = new System.Drawing.Point(10, 54);
            this.pnlCodeList.Size = new System.Drawing.Size(280, 426);
            this.pnlCodeList.BackColor = System.Drawing.Color.Transparent;
            this.pnlCodeList.Name = "pnlCodeList";
            this.pnlCodeList.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCodeList, this.lstCodeBlocks });

            SetupListHeader(this.lblCodeList, "Задания");
            SetupListBox(this.lstCodeBlocks, "lstCodeBlocks");
            this.lstCodeBlocks.SelectedIndexChanged += new System.EventHandler(this.lstCodeBlocks_SelectedIndexChanged);

            this.pnlCodeForm.Location = new System.Drawing.Point(310, 54);
            this.pnlCodeForm.Size = new System.Drawing.Size(760, 370);
            this.pnlCodeForm.BackColor = System.Drawing.Color.White;
            this.pnlCodeForm.Name = "pnlCodeForm";
            this.pnlCodeForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCode, this.txtCode,
                this.lblExpected, this.txtExpected,
                this.lblLang, this.txtLang,
                this.lblCodeOrder, this.numCodeOrder });

            SetupLabel(this.lblCode, "Код задания (C#)", new System.Drawing.Point(14, 12));
            SetupTextBoxML(this.txtCode, "txtCode", new System.Drawing.Point(14, 32), new System.Drawing.Size(730, 120));
            this.txtCode.Font = new System.Drawing.Font("Consolas", 9F);

            SetupLabel(this.lblExpected, "Эталонный вывод", new System.Drawing.Point(14, 160));
            SetupTextBoxML(this.txtExpected, "txtExpected", new System.Drawing.Point(14, 180), new System.Drawing.Size(730, 60));
            this.txtExpected.Font = new System.Drawing.Font("Consolas", 9F);

            SetupLabel(this.lblLang, "Язык", new System.Drawing.Point(14, 248));
            SetupTextBox(this.txtLang, "txtLang", new System.Drawing.Point(14, 268), new System.Drawing.Size(200, 26));

            SetupLabel(this.lblCodeOrder, "Порядок (#)", new System.Drawing.Point(14, 302));
            SetupNumeric(this.numCodeOrder, "numCodeOrder", new System.Drawing.Point(14, 322));

            this.pnlCodeBtns.Location = new System.Drawing.Point(310, 432);
            this.pnlCodeBtns.Size = new System.Drawing.Size(500, 40);
            this.pnlCodeBtns.BackColor = System.Drawing.Color.Transparent;
            this.pnlCodeBtns.Name = "pnlCodeBtns";
            this.pnlCodeBtns.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnCodeAdd, this.btnCodeSave, this.btnCodeDelete });

            SetupButton(this.btnCodeAdd, "btnCodeAdd", "＋ Добавить", System.Drawing.Color.FromArgb(67, 160, 71), new System.Drawing.Point(0, 0));
            SetupButton(this.btnCodeSave, "btnCodeSave", "💾 Сохранить", System.Drawing.Color.FromArgb(45, 125, 210), new System.Drawing.Point(148, 0));
            SetupButton(this.btnCodeDelete, "btnCodeDelete", "🗑 Удалить", System.Drawing.Color.FromArgb(229, 57, 53), new System.Drawing.Point(296, 0));

            this.btnCodeAdd.Click += new System.EventHandler(this.btnCodeAdd_Click);
            this.btnCodeSave.Click += new System.EventHandler(this.btnCodeSave_Click);
            this.btnCodeDelete.Click += new System.EventHandler(this.btnCodeDelete_Click);

            
            
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Панель администратора — Управление контентом";
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlTopBar);
            this.Load += new System.EventHandler(this.AdminForm_Load);

            
            
            
            ((System.ComponentModel.ISupportInitialize)this.numChOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numTopOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numLesOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numCodeOrder).EndInit();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.tabChapters.ResumeLayout(false);
            this.tabTopics.ResumeLayout(false);
            this.tabLessons.ResumeLayout(false);
            this.tabCodeBlocks.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        
        
        
        private static void SetupLabel(System.Windows.Forms.Label lbl, string text, System.Drawing.Point loc)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            lbl.ForeColor = System.Drawing.Color.FromArgb(100, 108, 120);
            lbl.Location = loc;
            lbl.Text = text;
        }

        private static void SetupFilterLabel(System.Windows.Forms.Label lbl, string text, System.Drawing.Point loc)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(45, 125, 210);
            lbl.Location = loc;
            lbl.Text = text;
        }

        private static void SetupTextBox(System.Windows.Forms.TextBox tb, string name,
            System.Drawing.Point loc, System.Drawing.Size size)
        {
            tb.Name = name;
            tb.Location = loc;
            tb.Size = size;
            tb.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.BackColor = System.Drawing.Color.FromArgb(246, 248, 251);
        }

        private static void SetupTextBoxML(System.Windows.Forms.TextBox tb, string name,
            System.Drawing.Point loc, System.Drawing.Size size)
        {
            SetupTextBox(tb, name, loc, size);
            tb.Multiline = true;
            tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        }

        private static void SetupNumeric(System.Windows.Forms.NumericUpDown num, string name,
            System.Drawing.Point loc)
        {
            num.Name = name;
            num.Location = loc;
            num.Size = new System.Drawing.Size(100, 26);
            num.Minimum = 0;
            num.Maximum = 9999;
            num.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        }

        private static void SetupCombo(System.Windows.Forms.ComboBox cmb, string name,
            System.Drawing.Point loc)
        {
            cmb.Name = name;
            cmb.Location = loc;
            cmb.Size = new System.Drawing.Size(360, 28);
            cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmb.BackColor = System.Drawing.Color.White;
        }

        private static void SetupListHeader(System.Windows.Forms.Label lbl, string text)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(45, 125, 210);
            lbl.Location = new System.Drawing.Point(0, 0);
            lbl.Text = text;
        }

        private static void SetupListBox(System.Windows.Forms.ListBox lb, string name)
        {
            lb.Name = name;
            lb.Location = new System.Drawing.Point(0, 24);
            lb.Size = new System.Drawing.Size(280, 396);
            lb.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lb.BackColor = System.Drawing.Color.White;
            lb.ItemHeight = 26;
        }

        private static void SetupButton(System.Windows.Forms.Button btn, string name,
            string text, System.Drawing.Color back, System.Drawing.Point loc)
        {
            btn.Name = name;
            btn.Text = text;
            btn.Location = loc;
            btn.Size = new System.Drawing.Size(140, 36);
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.BackColor = back;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
        }

        
        
        
        private AdminTopBar pnlTopBar;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Button btnBackToMain;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabChapters;
        private System.Windows.Forms.TabPage tabTopics;
        private System.Windows.Forms.TabPage tabLessons;
        private System.Windows.Forms.TabPage tabCodeBlocks;

        
        private System.Windows.Forms.Panel pnlChapterList, pnlChapterForm, pnlChapterBtns;
        private System.Windows.Forms.Label lblChapterList, lblChTitle, lblChDesc, lblChCoverUrl, lblChOrder;
        private System.Windows.Forms.ListBox lstChapters;
        private System.Windows.Forms.TextBox txtChTitle, txtChDesc, txtChCoverUrl;
        private System.Windows.Forms.NumericUpDown numChOrder;
        private System.Windows.Forms.Button btnChAdd, btnChSave, btnChDelete;

        
        private System.Windows.Forms.Panel pnlTopicFilter, pnlTopicList, pnlTopicForm, pnlTopicBtns;
        private System.Windows.Forms.Label lblTopicChFilter, lblTopicList, lblTopTitle, lblTopDesc, lblTopOrder;
        private System.Windows.Forms.ComboBox cmbTopicChapter;
        private System.Windows.Forms.ListBox lstTopics;
        private System.Windows.Forms.TextBox txtTopTitle, txtTopDesc;
        private System.Windows.Forms.NumericUpDown numTopOrder;
        private System.Windows.Forms.Button btnTopAdd, btnTopSave, btnTopDelete;

        
        private System.Windows.Forms.Panel pnlLessonFilter, pnlLessonList, pnlLessonForm, pnlLessonBtns;
        private System.Windows.Forms.Label lblLessonTopFilter, lblLessonList, lblLesTitle, lblLesContent, lblLesOrder;
        private System.Windows.Forms.ComboBox cmbLessonTopic;
        private System.Windows.Forms.ListBox lstLessons;
        private System.Windows.Forms.TextBox txtLesTitle, txtLesContent;
        private System.Windows.Forms.NumericUpDown numLesOrder;
        private System.Windows.Forms.Button btnLesAdd, btnLesSave, btnLesDelete;

        
        private System.Windows.Forms.Panel pnlCodeFilter, pnlCodeList, pnlCodeForm, pnlCodeBtns;
        private System.Windows.Forms.Label lblCodeLesFilter, lblCodeList, lblCode, lblExpected, lblLang, lblCodeOrder;
        private System.Windows.Forms.ComboBox cmbCodeLesson;
        private System.Windows.Forms.ListBox lstCodeBlocks;
        private System.Windows.Forms.TextBox txtCode, txtExpected, txtLang;
        private System.Windows.Forms.NumericUpDown numCodeOrder;
        private System.Windows.Forms.Button btnCodeAdd, btnCodeSave, btnCodeDelete;
    }
}