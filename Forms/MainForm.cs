using CSharpDesctop.Services;
using CSharpDesctop.Models;
using CSharpDesctop.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    public partial class MainForm : Form
    {
        private const string CurrentLocalVersion = "1.0.0";

        
        private static readonly Color[] ChapterAccents =
        {
            Color.FromArgb(45, 125, 210),   
            Color.FromArgb(67, 160, 71),    
            Color.FromArgb(142, 68, 173),   
            Color.FromArgb(211, 84, 0),     
            Color.FromArgb(41, 128, 185),   
            Color.FromArgb(192, 57, 43),    
        };

        public MainForm()
        {
            InitializeComponent();
            typeof(FlowLayoutPanel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
    ?.SetValue(pnlFlowGrid, true, null);
            btnProfile.Visible = (UserSession.UserId != null);
            button1.Visible = (UserSession.UserId == null);
            btnAdminPanel.Visible = UserSession.IsAdmin;
            CheckAppVersionAsync();
        }

        private async Task CheckAppVersionAsync()
        {
            try
            {
                await Task.Delay(1500);
                string serverVersionData = await DatabaseHelper.GetLatestDesktopVersionAsync();
                if (!string.IsNullOrEmpty(serverVersionData))
                {
                    string[] parts = serverVersionData.Split('|');
                    if (parts.Length >= 3 && parts[0] != CurrentLocalVersion)
                    {
                        MessageBox.Show(
                            $"Доступна новая версия {parts[0]}!\n\nСписок изменений:\n{parts[1]}\n\nСкачать: {parts[2]}",
                            "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка проверки обновлений: {ex.Message}");
            }
        }

        private async Task LoadChaptersAsync()
        {
            try
            {
                List<ChapterModel> chapters = await DatabaseHelper.GetChaptersAsync();
                List<TopicModel> allTopics = await DatabaseHelper.GetAllTopicsAsync();

                pnlFlowGrid.Controls.Clear();

                if (chapters == null || chapters.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "Данные учебного пособия не найдены или нет сети.",
                        Size = new Size(500, 30),
                        ForeColor = Color.FromArgb(180, 60, 60),
                        Font = new Font("Segoe UI", 10F),
                        Location = new Point(20, 20)
                    };
                    pnlFlowGrid.Controls.Add(lblEmpty);
                    return;
                }

                int colorIndex = 0;
                foreach (var chapter in chapters)
                {
                    Color accent = ChapterAccents[colorIndex % ChapterAccents.Length];
                    colorIndex++;

                    Panel card = CreateChapterCard(chapter, allTopics, accent);
                    pnlFlowGrid.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateChapterCard(ChapterModel chapter, List<TopicModel> allTopics, Color accent)
        {
            
            var card = new AnimatedCard
            {
                Size = new Size(420, 170),
                Margin = new Padding(10),
                Accent = accent
            };

            
            var stripe = new Panel
            {
                Size = new Size(5, 170),
                Location = new Point(0, 0),
                BackColor = accent
            };
            stripe.Paint += (s, e) => e.Graphics.Clear(accent);

            
            int chNum = 1;
            var firstTopic = allTopics.FirstOrDefault(t => t.ChapterId == chapter.Id);
            if (firstTopic != null)
            {
                chNum = (allTopics.IndexOf(firstTopic) / 1) + 1;
            }

            
            var lblNum = new Label
            {
                Text = chNum.ToString(),
                AutoSize = false,
                Size = new Size(36, 36),
                Location = new Point(20, 14),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = accent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblNum.Paint += (s, ev) =>
            {
                ev.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                ev.Graphics.Clear(card.BackColor);
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, 35, 35);
                    using (var brush = new SolidBrush(accent))
                        ev.Graphics.FillPath(brush, path);
                }
                TextRenderer.DrawText(ev.Graphics, lblNum.Text, lblNum.Font,
                    new Rectangle(0, 0, 36, 36), Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            var title = new Label
            {
                Text = chapter.Title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 35, 45),
                Location = new Point(66, 14),
                Size = new Size(340, 26),
                AutoEllipsis = true
            };

            var desc = new Label
            {
                Text = string.IsNullOrEmpty(chapter.Description) ? "Теоретический и практический материал по теме." : chapter.Description,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 108, 120),
                Location = new Point(66, 44),
                Size = new Size(340, 46),
                AutoEllipsis = true
            };

            var divider = new Panel
            {
                Size = new Size(390, 1),
                Location = new Point(15, 100),
                BackColor = Color.FromArgb(230, 233, 238)
            };

            
            var cmb = new StyledComboBox
            {
                Location = new Point(15, 112),
                Size = new Size(388, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = Color.FromArgb(246, 248, 251),
                ForeColor = Color.FromArgb(50, 55, 65)
            };

            var chapterTopics = allTopics.Where(t => t.ChapterId == chapter.Id).ToList();
            var src = new List<TopicModel> { new TopicModel { Id = Guid.Empty, Title = "📖 Выбрать тему..." } };
            src.AddRange(chapterTopics);

            cmb.DataSource = src;
            cmb.DisplayMember = "Title";
            cmb.ValueMember = "Id";


            card.Controls.AddRange(new Control[] { stripe, lblNum, title, desc, divider, cmb });

            cmb.SelectedIndexChanged += (s, ev) =>
            {
                if (cmb.SelectedItem is TopicModel t && t.Id != Guid.Empty)
                {
                    new LessonForm(t.Id, t.Title).Show();
                    cmb.BeginInvoke(new Action(() => cmb.SelectedIndex = 0));
                }
            };
            cmb.MouseWheel += (s, e) =>
            {
                ((HandledMouseEventArgs)e).Handled = true;
            };

            return card;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            using (ProfileForm profileForm = new ProfileForm())
            {
                if (profileForm.ShowDialog() == DialogResult.OK)
                {
                    AuthForm authForm = new AuthForm();
                    authForm.Show();
                    this.FormClosed -= MainForm_FormClosed;
                    this.Close();
                }
            }
        }
        private void btnAdminPanel_Click(object sender, EventArgs e) => MessageBox.Show("Доступ к CRUD-операциям таблиц БД разрешен.", "Панель администратора");
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

        private async void MainForm_Load(object sender, EventArgs e)
        {

            var lblLoading = new Label
            {
                Text = "⏳ Загрузка учебных материалов...",
                Size = new Size(400, 30),
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 110, 130)
            };
            pnlFlowGrid.Controls.Add(lblLoading);
            await LoadChaptersAsync();
        }

        private void pnlFlowGrid_Paint(object sender, PaintEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm();
            authForm.Show();
            this.Hide();
        }
    }

    
    
    
    internal class AnimatedCard : Panel
    {
        public Color Accent { get; set; } = Color.FromArgb(45, 125, 210);

        private float _lift = 0f;
        private bool _hovered = false;
        private readonly System.Windows.Forms.Timer _timer;

        public AnimatedCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;
            Cursor = Cursors.Hand;

            _timer = new System.Windows.Forms.Timer { Interval = 16 };
            _timer.Tick += (s, e) =>
            {
                float target = _hovered ? 1f : 0f;
                _lift += (_hovered ? 0.12f : -0.12f);
                _lift = Math.Max(0f, Math.Min(1f, _lift));
                if (_lift == target) _timer.Stop();
                Invalidate();
            };

            MouseEnter += (s, e) => { _hovered = true; _timer.Start(); };
            MouseLeave += (s, e) => { _hovered = false; _timer.Start(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            
            Color parentBg = Parent?.BackColor ?? Color.FromArgb(240, 244, 248);
            g.Clear(parentBg);

            
            int liftOffset = (int)(-3 * _lift);
            Rectangle rect = new Rectangle(2, 2 + liftOffset, Width - 8, Height - 8);

            
            int shadowAlpha = (int)(15 + 35 * _lift);
            for (int i = 3; i >= 1; i--)
            {
                Rectangle shadowRect = new Rectangle(rect.X + i, rect.Y + i + (int)(2 * _lift), rect.Width, rect.Height);
                using (var shadowPath = RoundRect(shadowRect, 14))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(shadowAlpha / i, 50, 80, 150)))
                    g.FillPath(shadowBrush, shadowPath);
            }

            
            using (var path = RoundRect(rect, 14))
            using (var brush = new SolidBrush(Color.White))
                g.FillPath(brush, path);

            
            Color borderColor = _lift > 0
                ? Color.FromArgb(
                    (int)(225 + (Accent.R - 225) * _lift * 0.7f),
                    (int)(230 + (Accent.G - 230) * _lift * 0.7f),
                    (int)(235 + (Accent.B - 235) * _lift * 0.7f))
                : Color.FromArgb(225, 230, 235);

            using (var path = RoundRect(rect, 14))
            using (var pen = new Pen(borderColor, 1.5f))
                g.DrawPath(pen, path);
        }

        private static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _timer?.Dispose();
            base.Dispose(disposing);
        }
    }

    
    
    
    internal class StyledComboBox : ComboBox
    {
        public StyledComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 28;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bg = selected ? Color.FromArgb(45, 125, 210) : Color.White;
            Color fg = selected ? Color.White : Color.FromArgb(50, 55, 65);

            e.Graphics.FillRectangle(new SolidBrush(bg), e.Bounds);

            
            string text = "";
            if (Items[e.Index] is TopicModel topic)
            {
                text = topic.Title; 
            }
            else
            {
                text = Items[e.Index]?.ToString() ?? "";
            }

            TextRenderer.DrawText(e.Graphics, text, Font, e.Bounds, fg,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding);

            e.DrawFocusRectangle();
        }
    }
}