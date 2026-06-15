using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnAdminPanel;
        private System.Windows.Forms.FlowLayoutPanel pnlFlowGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlTopBar = new GradientTopBar();
            button1 = new Button();
            btnAdminPanel = new Button();
            btnProfile = new Button();
            lblBanner = new Label();
            pnlFlowGrid = new FlowLayoutPanel();
            pnlTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.Controls.Add(button1);
            pnlTopBar.Controls.Add(btnAdminPanel);
            pnlTopBar.Controls.Add(btnProfile);
            pnlTopBar.Controls.Add(lblBanner);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(947, 76);
            pnlTopBar.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(192, 192, 255);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(120, 255, 255, 255);
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 255, 255, 255);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(827, 18);
            button1.Name = "button1";
            button1.Size = new Size(104, 40);
            button1.TabIndex = 3;
            button1.Text = "Вернуться к Авторизации";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnAdminPanel
            // 
            btnAdminPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdminPanel.BackColor = Color.FromArgb(229, 57, 53);
            btnAdminPanel.Cursor = Cursors.Hand;
            btnAdminPanel.FlatAppearance.BorderSize = 0;
            btnAdminPanel.FlatStyle = FlatStyle.Flat;
            btnAdminPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdminPanel.ForeColor = Color.White;
            btnAdminPanel.Location = new Point(705, 20);
            btnAdminPanel.Name = "btnAdminPanel";
            btnAdminPanel.Size = new Size(114, 36);
            btnAdminPanel.TabIndex = 0;
            btnAdminPanel.Text = "Админ-панель";
            btnAdminPanel.UseVisualStyleBackColor = false;
            btnAdminPanel.Click += btnAdminPanel_Click;
            // 
            // btnProfile
            // 
            btnProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProfile.BackColor = Color.FromArgb(192, 192, 255);
            btnProfile.Cursor = Cursors.Hand;
            btnProfile.FlatAppearance.BorderColor = Color.FromArgb(120, 255, 255, 255);
            btnProfile.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 255, 255, 255);
            btnProfile.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnProfile.ForeColor = Color.White;
            btnProfile.Location = new Point(827, 20);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(104, 36);
            btnProfile.TabIndex = 1;
            btnProfile.Text = "👤 Профиль";
            btnProfile.UseVisualStyleBackColor = false;
            btnProfile.Click += btnProfile_Click;
            // 
            // lblBanner
            // 
            lblBanner.BackColor = Color.Transparent;
            lblBanner.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblBanner.ForeColor = Color.White;
            lblBanner.Location = new Point(18, 16);
            lblBanner.Name = "lblBanner";
            lblBanner.Size = new Size(380, 44);
            lblBanner.TabIndex = 2;
            lblBanner.Text = "📘 C# Manual";
            // 
            // pnlFlowGrid
            // 
            pnlFlowGrid.AutoScroll = true;
            pnlFlowGrid.BackColor = Color.FromArgb(240, 244, 249);
            pnlFlowGrid.Dock = DockStyle.Fill;
            pnlFlowGrid.Location = new Point(0, 76);
            pnlFlowGrid.Name = "pnlFlowGrid";
            pnlFlowGrid.Padding = new Padding(16, 18, 16, 18);
            pnlFlowGrid.Size = new Size(947, 486);
            pnlFlowGrid.TabIndex = 0;
            pnlFlowGrid.Paint += pnlFlowGrid_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 244, 249);
            ClientSize = new Size(947, 562);
            Controls.Add(pnlFlowGrid);
            Controls.Add(pnlTopBar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# Manual — Электронное пособие";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            pnlTopBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GradientTopBar pnlTopBar;
        private Button button1;
    }

    /// <summary>
    /// Шапка с градиентом от насыщенного синего к более тёмному.
    /// </summary>
    internal class GradientTopBar : Panel
    {
        public GradientTopBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            using (var brush = new LinearGradientBrush(rect,
                Color.FromArgb(30, 90, 180),
                Color.FromArgb(55, 130, 230),
                LinearGradientMode.Horizontal))
                e.Graphics.FillRectangle(brush, rect);

            // Тонкая линия снизу
            using (var pen = new Pen(Color.FromArgb(20, 0, 0, 60), 1))
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            base.OnPaint(e);
        }
    }
}