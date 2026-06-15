using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlLoginCard;
        private System.Windows.Forms.Panel pnlRegisterCard;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnGuest;
        private System.Windows.Forms.Label lblRegisterLink;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.TextBox txtRegLogin;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.TextBox txtRegEmail;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.TextBox txtRegPasswordConfirm;
        private System.Windows.Forms.Button btnRegisterSubmit;
        private System.Windows.Forms.Label lblLoginLink;
        private System.Windows.Forms.Timer tmrAnimation;
        private System.Windows.Forms.Panel pnlPasswordStrength;
        private System.Windows.Forms.Panel pbStrengthFill;
        private System.Windows.Forms.Label lblStrengthText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));

            components = new System.ComponentModel.Container();

            // --- Левая часть: брендинг ---
            var pnlBrand = new GradientBrandPanel();

            var lblAppName = new Label
            {
                Text = "C# Manual",
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(50, 180)
            };
            var lblAppSub = new Label
            {
                Text = "Электронный учебный справочник\nпо языку программирования C#",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(200, 230, 255),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(50, 234)
            };
            var lblCodeDecor = new Label
            {
                Text = "{ } // <>",
                Font = new Font("Consolas", 48F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 255, 255, 255),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(30, 310)
            };
            pnlBrand.Controls.AddRange(new Control[] { lblCodeDecor, lblAppName, lblAppSub });

            // --- Правая часть: карточки ---
            var pnlRight = new Panel
            {
                BackColor = Color.FromArgb(245, 248, 252),
                Dock = DockStyle.Right,
                Width = 420
            };

            // LOGIN CARD
            pnlLoginCard = new RoundedCard
            {
                Size = new Size(360, 430),
                Location = new Point(30, 60)
            };

            label3 = new Label
            {
                Text = "Добро пожаловать",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 35, 55),
                AutoSize = true,
                Location = new Point(24, 28)
            };
            var lblSub = new Label
            {
                Text = "Войдите в свой аккаунт",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(130, 140, 160),
                AutoSize = true,
                Location = new Point(24, 58)
            };

            label1 = MakeFieldLabel("Логин", new Point(24, 95));
            txtLogin = MakeTextBox(new Point(24, 115), 312);

            label2 = MakeFieldLabel("Пароль", new Point(24, 157));
            txtPassword = MakeTextBox(new Point(24, 177), 270);
            chkRememberMe = MakeCheckBox(new Point(24, 207), 270);

            txtPassword.UseSystemPasswordChar = true;

            btnShowPassword = new Button
            {
                Location = new Point(298, 177),
                Size = new Size(38, 26),
                Text = "👁",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 243, 247),
                ForeColor = Color.FromArgb(80, 90, 110),
                Cursor = Cursors.Hand,
                TabIndex = 0
            };
            btnShowPassword.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 225);
            btnShowPassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 228, 240);
            btnShowPassword.Click += btnShowPassword_Click;

            btnLogin = MakePrimaryButton("Войти", new Point(24, 242), 312);
            btnLogin.Click += btnLogin_Click;

            var divLine = new Panel { Size = new Size(312, 1), Location = new Point(24, 295), BackColor = Color.FromArgb(225, 228, 235) };
            var lblOr = new Label { Text = "или", Font = new Font("Segoe UI", 8.5F), ForeColor = Color.FromArgb(160, 168, 180), AutoSize = true, Location = new Point(165, 287) };
            lblOr.BackColor = Color.White; // перекрывает линию

            btnGuest = MakeSecondaryButton("Режим гостя", new Point(24, 310), 312);
            btnGuest.Click += btnGuest_Click;

            lblRegisterLink = new Label
            {
                Text = "Нет аккаунта? Зарегистрироваться →",
                ForeColor = Color.FromArgb(45, 125, 210),
                Font = new Font("Segoe UI", 9F, FontStyle.Underline),
                AutoSize = true,
                Location = new Point(70, 370),
                Cursor = Cursors.Hand
            };
            lblRegisterLink.Click += lblRegisterLink_Click;

            pnlLoginCard.Controls.AddRange(new Control[] {
                label3, chkRememberMe, lblSub, label1, txtLogin, label2, txtPassword, btnShowPassword,
                btnLogin, divLine, lblOr, btnGuest, lblRegisterLink
            });

            // REGISTER CARD
            pnlRegisterCard = new RoundedCard
            {
                Size = new Size(360, 530),
                Location = new Point(30, 30),
                Visible = false
            };

            label4 = new Label
            {
                Text = "Создать аккаунт",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 35, 55),
                AutoSize = true,
                Location = new Point(24, 24)
            };

            label5 = MakeFieldLabel("Логин", new Point(24, 65));
            txtRegLogin = MakeTextBox(new Point(24, 83), 312);

            label6 = MakeFieldLabel("Имя", new Point(24, 119));
            txtRegName = MakeTextBox(new Point(24, 137), 312);

            label7 = MakeFieldLabel("Email", new Point(24, 172));
            txtRegEmail = MakeTextBox(new Point(24, 190), 312);

            label8 = MakeFieldLabel("Пароль", new Point(24, 225));
            txtRegPassword = MakeTextBox(new Point(24, 243), 312);
            txtRegPassword.UseSystemPasswordChar = true;
            txtRegPassword.TextChanged += txtRegPassword_TextChanged;

            pnlPasswordStrength = new Panel
            {
                Location = new Point(24, 272),
                Size = new Size(312, 4),
                BackColor = Color.FromArgb(225, 228, 235)
            };
            pbStrengthFill = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(0, 4),
                BackColor = Color.FromArgb(220, 50, 50)
            };
            pnlPasswordStrength.Controls.Add(pbStrengthFill);

            lblStrengthText = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(130, 140, 160),
                AutoSize = true,
                Location = new Point(24, 280)
            };

            label9 = MakeFieldLabel("Подтвердите пароль", new Point(24, 299));
            txtRegPasswordConfirm = MakeTextBox(new Point(24, 317), 270);
            txtRegPasswordConfirm.UseSystemPasswordChar = true;

            button1 = new Button
            {
                Location = new Point(298, 317),
                Size = new Size(38, 26),
                Text = "👁",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 243, 247),
                ForeColor = Color.FromArgb(80, 90, 110),
                Cursor = Cursors.Hand,
                TabIndex = 15
            };
            button1.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 225);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 228, 240);
            button1.Click += button1_Click;

            btnRegisterSubmit = MakePrimaryButton("Зарегистрироваться", new Point(24, 360), 312);
            btnRegisterSubmit.Click += btnRegisterSubmit_Click;

            lblLoginLink = new Label
            {
                Text = "Уже есть аккаунт? Войти →",
                ForeColor = Color.FromArgb(45, 125, 210),
                Font = new Font("Segoe UI", 9F, FontStyle.Underline),
                AutoSize = true,
                Location = new Point(96, 420),
                Cursor = Cursors.Hand
            };
            lblLoginLink.Click += lblLoginLink_Click;

            pnlRegisterCard.Controls.AddRange(new Control[] {
                label4, label5, txtRegLogin, label6, txtRegName, label7, txtRegEmail,
                label8, txtRegPassword, pnlPasswordStrength, lblStrengthText, label9, txtRegPasswordConfirm, button1,
                btnRegisterSubmit, lblLoginLink
            });

            pnlRight.Controls.Add(pnlLoginCard);
            pnlRight.Controls.Add(pnlRegisterCard);

            // Timer
            tmrAnimation = new System.Windows.Forms.Timer(components) { Interval = 40 };

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(831, 544);
            Controls.Add(pnlBrand);
            Controls.Add(pnlRight);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# Manual — Авторизация";
            Load += AuthForm_Load;
        }

        private static Label MakeFieldLabel(string text, Point loc) => new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(80, 90, 110),
            AutoSize = true,
            Location = loc
        };

        private static TextBox MakeTextBox(Point loc, int width) => new TextBox
        {
            Location = loc,
            Size = new Size(width, 26),
            Font = new Font("Segoe UI", 10F),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(248, 250, 253)
        };

        private static CheckBox MakeCheckBox(Point loc, int width) => new CheckBox
        {
            Location = loc,
            Size = new Size(width, 26),
            Font = new Font("Segoe UI", 10F),
            BackColor = Color.FromArgb(248, 250, 253),
            Text = "Запомнить меня?"
        };

        private static Button MakePrimaryButton(string text, Point loc, int width)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                Size = new Size(width, 40),
                BackColor = Color.FromArgb(45, 125, 210),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 140, 225);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 100, 185);
            return btn;
        }

        private static Button MakeSecondaryButton(string text, Point loc, int width)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                Size = new Size(width, 38),
                BackColor = Color.FromArgb(240, 244, 250),
                ForeColor = Color.FromArgb(55, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(210, 216, 226);
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 232, 245);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(208, 220, 238);
            return btn;
        }

        #endregion

        private Label label3, label2, label1;
        private Label label9, label8, label7, label6, label5, label4;
        private Button button1;
    }

    // Карточка с белым фоном, скруглёнными углами и тенью
    internal class RoundedCard : Panel
    {
        public RoundedCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent?.BackColor ?? Color.FromArgb(245, 248, 252));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(2, 2, Width - 5, Height - 5);

            // Тень
            for (int i = 4; i >= 1; i--)
            {
                var sr = new Rectangle(rect.X + i, rect.Y + i, rect.Width, rect.Height);
                using (var sp = RoundPath(sr, 16))
                using (var sb = new SolidBrush(Color.FromArgb(10 + i * 4, 60, 80, 150)))
                    g.FillPath(sb, sp);
            }

            // Заливка
            using (var path = RoundPath(rect, 16))
            using (var brush = new SolidBrush(Color.White))
                g.FillPath(brush, path);

            // Рамка
            using (var path = RoundPath(rect, 16))
            using (var pen = new Pen(Color.FromArgb(220, 225, 235), 1f))
                g.DrawPath(pen, path);
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundPath(Rectangle r, int radius)
        {
            int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // Левая брендовая панель с градиентом
    internal class GradientBrandPanel : Panel
    {
        public GradientBrandPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            Dock = DockStyle.Fill;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            using (var brush = new LinearGradientBrush(rect,
                Color.FromArgb(18, 60, 140),
                Color.FromArgb(45, 125, 210),
                LinearGradientMode.ForwardDiagonal))
                e.Graphics.FillRectangle(brush, rect);

            // Декоративные круги
            DrawCircle(e.Graphics, -60, -60, 300, 10);
            DrawCircle(e.Graphics, Width - 100, Height - 100, 250, 8);
            DrawCircle(e.Graphics, 80, Height / 2, 150, 6);

            base.OnPaint(e);
        }

        private static void DrawCircle(Graphics g, int cx, int cy, int r, int alpha)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(alpha, 255, 255, 255), 2f))
                g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
        }
    }
}