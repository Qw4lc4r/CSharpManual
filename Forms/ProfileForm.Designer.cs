using CSharpDesctop.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserLogin;
        private CSharpDesctop.Controls.RoundedProgressBar progressBar;
        private System.Windows.Forms.Label lblStats;
        private CSharpDesctop.Controls.RoundedButton btnReset;

        private System.Windows.Forms.PictureBox picAvatar;
        private CSharpDesctop.Controls.RoundedButton btnChangeAvatar;
        private System.Windows.Forms.TextBox txtEditName;
        private System.Windows.Forms.TextBox txtEditEmail;
        private CSharpDesctop.Controls.RoundedButton btnSaveChanges;
        private CSharpDesctop.Controls.RoundedButton btnChangePassword;
        private CSharpDesctop.Controls.RoundedButton btnAboutApp;

        private CSharpDesctop.Controls.RoundedPanel pnlHeaderCard;
        private CSharpDesctop.Controls.RoundedPanel pnlStatsCard;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            picAvatar = new PictureBox();
            btnChangeAvatar = new RoundedButton();
            lblUserLogin = new Label();
            txtEditName = new TextBox();
            txtEditEmail = new TextBox();
            btnSaveChanges = new RoundedButton();
            progressBar = new RoundedProgressBar();
            lblStats = new Label();
            btnChangePassword = new RoundedButton();
            btnReset = new RoundedButton();
            btnAboutApp = new RoundedButton();
            pnlHeaderCard = new RoundedPanel();
            lblName = new Label();
            lblEmail = new Label();
            pnlStatsCard = new RoundedPanel();
            lblStatsTitle = new Label();
            roundedButton1 = new RoundedButton();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            pnlHeaderCard.SuspendLayout();
            pnlStatsCard.SuspendLayout();
            SuspendLayout();
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.FromArgb(214, 232, 250);
            picAvatar.Location = new Point(20, 20);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(76, 76);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 6;
            picAvatar.TabStop = false;
            // 
            // btnChangeAvatar
            // 
            btnChangeAvatar.BackColor = Color.FromArgb(45, 125, 210);
            btnChangeAvatar.CornerRadius = 8;
            btnChangeAvatar.FlatStyle = FlatStyle.Flat;
            btnChangeAvatar.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChangeAvatar.ForeColor = Color.White;
            btnChangeAvatar.Location = new Point(14, 102);
            btnChangeAvatar.Name = "btnChangeAvatar";
            btnChangeAvatar.ShowShadow = false;
            btnChangeAvatar.Size = new Size(88, 28);
            btnChangeAvatar.TabIndex = 7;
            btnChangeAvatar.Text = "📷 Фото";
            btnChangeAvatar.UseVisualStyleBackColor = false;
            btnChangeAvatar.Click += btnChangeAvatar_Click;
            // 
            // lblUserLogin
            // 
            lblUserLogin.AutoSize = true;
            lblUserLogin.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblUserLogin.ForeColor = Color.FromArgb(140, 148, 165);
            lblUserLogin.Location = new Point(114, 48);
            lblUserLogin.Name = "lblUserLogin";
            lblUserLogin.Size = new Size(79, 17);
            lblUserLogin.TabIndex = 3;
            lblUserLogin.Text = "Логин: guest";
            // 
            // txtEditName
            // 
            txtEditName.BackColor = Color.White;
            txtEditName.BorderStyle = BorderStyle.None;
            txtEditName.Font = new Font("Segoe UI", 10F);
            txtEditName.Location = new Point(0, 0);
            txtEditName.Name = "txtEditName";
            txtEditName.PlaceholderText = "Ваше имя (ФИО)";
            txtEditName.Size = new Size(320, 18);
            txtEditName.TabIndex = 0;
            // 
            // txtEditEmail
            // 
            txtEditEmail.BackColor = Color.White;
            txtEditEmail.BorderStyle = BorderStyle.None;
            txtEditEmail.Font = new Font("Segoe UI", 10F);
            txtEditEmail.Location = new Point(0, 0);
            txtEditEmail.Name = "txtEditEmail";
            txtEditEmail.PlaceholderText = "Email";
            txtEditEmail.Size = new Size(320, 18);
            txtEditEmail.TabIndex = 0;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.BackColor = Color.FromArgb(45, 125, 210);
            btnSaveChanges.CornerRadius = 10;
            btnSaveChanges.FlatStyle = FlatStyle.Flat;
            btnSaveChanges.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSaveChanges.ForeColor = Color.White;
            btnSaveChanges.Location = new Point(20, 245);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.ShowShadow = true;
            btnSaveChanges.Size = new Size(348, 44);
            btnSaveChanges.TabIndex = 10;
            btnSaveChanges.Text = "Сохранить профиль";
            btnSaveChanges.UseVisualStyleBackColor = false;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // progressBar
            // 
            progressBar.CornerRadius = 8;
            progressBar.FillColorEnd = Color.FromArgb(86, 180, 233);
            progressBar.FillColorStart = Color.FromArgb(45, 125, 210);
            progressBar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            progressBar.Location = new Point(20, 50);
            progressBar.Maximum = 100;
            progressBar.Name = "progressBar";
            progressBar.ShowPercentText = true;
            progressBar.Size = new Size(348, 24);
            progressBar.TabIndex = 2;
            progressBar.TrackColor = Color.FromArgb(232, 236, 242);
            progressBar.Value = 0;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblStats.ForeColor = Color.FromArgb(75, 85, 100);
            lblStats.Location = new Point(20, 88);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(348, 98);
            lblStats.TabIndex = 1;
            lblStats.Text = "⏳ Загрузка статистики...";
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.FromArgb(45, 125, 210);
            btnChangePassword.CornerRadius = 10;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(18, 614);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.ShowShadow = true;
            btnChangePassword.Size = new Size(388, 44);
            btnChangePassword.TabIndex = 11;
            btnChangePassword.Text = "🔑 Сменить пароль";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(229, 57, 53);
            btnReset.CornerRadius = 10;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(18, 664);
            btnReset.Name = "btnReset";
            btnReset.ShowShadow = true;
            btnReset.Size = new Size(388, 44);
            btnReset.TabIndex = 0;
            btnReset.Text = "🔄 Сбросить прогресс обучения";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // btnAboutApp
            // 
            btnAboutApp.BackColor = Color.FromArgb(45, 125, 210);
            btnAboutApp.CornerRadius = 10;
            btnAboutApp.FlatStyle = FlatStyle.Flat;
            btnAboutApp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAboutApp.ForeColor = Color.White;
            btnAboutApp.Location = new Point(18, 564);
            btnAboutApp.Name = "btnAboutApp";
            btnAboutApp.ShowShadow = true;
            btnAboutApp.Size = new Size(388, 44);
            btnAboutApp.TabIndex = 12;
            btnAboutApp.Text = "ℹ️ О приложении";
            btnAboutApp.UseVisualStyleBackColor = false;
            btnAboutApp.Click += btnAboutApp_Click;
            // 
            // pnlHeaderCard
            // 
            pnlHeaderCard.BackColor = Color.White;
            pnlHeaderCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlHeaderCard.BorderThickness = 1;
            pnlHeaderCard.Controls.Add(picAvatar);
            pnlHeaderCard.Controls.Add(btnChangeAvatar);
            pnlHeaderCard.Controls.Add(lblName);
            pnlHeaderCard.Controls.Add(lblUserLogin);
            pnlHeaderCard.Controls.Add(lblEmail);
            pnlHeaderCard.Controls.Add(btnSaveChanges);
            pnlHeaderCard.CornerRadius = 16;
            pnlHeaderCard.HoverHighlight = true;
            pnlHeaderCard.Location = new Point(18, 18);
            pnlHeaderCard.Name = "pnlHeaderCard";
            pnlHeaderCard.Size = new Size(388, 305);
            pnlHeaderCard.TabIndex = 100;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(25, 35, 55);
            lblName.Location = new Point(114, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(94, 25);
            lblName.TabIndex = 13;
            lblName.Text = "Профиль";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(100, 116, 139);
            lblEmail.Location = new Point(20, 136);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(102, 15);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "Личные данные";
            // 
            // pnlStatsCard
            // 
            pnlStatsCard.BackColor = Color.White;
            pnlStatsCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlStatsCard.BorderThickness = 1;
            pnlStatsCard.Controls.Add(lblStatsTitle);
            pnlStatsCard.Controls.Add(progressBar);
            pnlStatsCard.Controls.Add(lblStats);
            pnlStatsCard.CornerRadius = 16;
            pnlStatsCard.HoverHighlight = true;
            pnlStatsCard.Location = new Point(18, 340);
            pnlStatsCard.Name = "pnlStatsCard";
            pnlStatsCard.Size = new Size(388, 205);
            pnlStatsCard.TabIndex = 101;
            // 
            // lblStatsTitle
            // 
            lblStatsTitle.AutoSize = true;
            lblStatsTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblStatsTitle.ForeColor = Color.FromArgb(25, 35, 55);
            lblStatsTitle.Location = new Point(20, 16);
            lblStatsTitle.Name = "lblStatsTitle";
            lblStatsTitle.Size = new Size(203, 21);
            lblStatsTitle.TabIndex = 12;
            lblStatsTitle.Text = "📊 Статистика обучения";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(45, 125, 210);
            roundedButton1.CornerRadius = 10;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(18, 714);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.ShowShadow = true;
            roundedButton1.Size = new Size(388, 44);
            roundedButton1.TabIndex = 102;
            roundedButton1.Text = "Выйти из аккаунта";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // ProfileForm
            // 
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(424, 776);
            Controls.Add(roundedButton1);
            Controls.Add(pnlHeaderCard);
            Controls.Add(pnlStatsCard);
            Controls.Add(btnChangePassword);
            Controls.Add(btnReset);
            Controls.Add(btnAboutApp);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ProfileForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление профилем";
            Load += ProfileForm_Load;
            Paint += ProfileForm_Paint;
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            pnlHeaderCard.ResumeLayout(false);
            pnlHeaderCard.PerformLayout();
            pnlStatsCard.ResumeLayout(false);
            pnlStatsCard.PerformLayout();
            ResumeLayout(false);
        }

        private void ProfileForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int step = 24;
            using (var dotBrush = new SolidBrush(Color.FromArgb(14, 45, 125, 210)))
            {
                for (int x = 8; x < Width; x += step)
                {
                    for (int y = 8; y < Height; y += step)
                    {
                        g.FillEllipse(dotBrush, x, y, 2, 2);
                    }
                }
            }
        }

        #endregion

        private RoundedButton roundedButton1;
    }
}