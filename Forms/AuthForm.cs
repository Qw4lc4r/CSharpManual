using CSharpDesctop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    public partial class AuthForm : Form
    {
        private bool _isToRegister = false;
        private int _animationStep = 0;
        private const int TotalSteps = 54;

        public AuthForm()
        {
            InitializeComponent();
            ConfigureAnimation();
        }

        private void ConfigureAnimation()
        {
            tmrAnimation.Interval = 10;
            tmrAnimation.Tick += TmrAnimation_Tick;
            if (pnlLoginCard.Parent is Panel parentPanel)
            {
                typeof(Panel).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.SetProperty |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic,
                    null, parentPanel, new object[] { true });
            }
        }

        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            _animationStep++;
            float progress = (float)_animationStep / TotalSteps;
            float ease = progress < 0.5f
                ? 4f * progress * progress * progress
                : 1f - (float)Math.Pow(-2f * progress + 2f, 3) / 2f;

            int containerWidth = pnlLoginCard.Parent?.Width ?? 420;
            int targetX = 30;

            if (_isToRegister)
            {
                pnlLoginCard.Left = (int)(targetX - (targetX + pnlLoginCard.Width) * ease);
                pnlRegisterCard.Left = (int)(containerWidth - (containerWidth - targetX) * ease);
            }
            else
            {
                pnlRegisterCard.Left = (int)(targetX + (containerWidth - targetX) * ease);
                pnlLoginCard.Left = (int)(-pnlLoginCard.Width + (targetX + pnlLoginCard.Width) * ease);
            }

            if (_animationStep >= TotalSteps)
            {
                tmrAnimation.Stop();
                if (_isToRegister)
                {
                    pnlLoginCard.Visible = false;
                    pnlRegisterCard.Left = targetX;
                }
                else
                {
                    pnlRegisterCard.Visible = false;
                    pnlLoginCard.Left = targetX;
                }
            }
        }

        private void lblRegisterLink_Click(object sender, EventArgs e)
        {
            if (tmrAnimation.Enabled) return;

            _isToRegister = true;
            _animationStep = 0;

            int containerWidth = pnlLoginCard.Parent?.Width ?? 420;
            pnlRegisterCard.Left = containerWidth;
            pnlRegisterCard.Visible = true;

            tmrAnimation.Start();
        }

        private void lblLoginLink_Click(object sender, EventArgs e)
        {
            if (tmrAnimation.Enabled) return;

            _isToRegister = false;
            _animationStep = 0;

            pnlLoginCard.Left = -pnlLoginCard.Width;
            pnlLoginCard.Visible = true;

            tmrAnimation.Start();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (await DatabaseHelper.AuthenticateAsync(txtLogin.Text, txtPassword.Text))
            {
                if (chkRememberMe.Checked && UserSession.UserId.HasValue)
                {
                    string currentGuid = GetMachineGuid();
                    await DatabaseHelper.UpdateMachineGuidAsync(UserSession.UserId.Value, currentGuid);
                    try { System.IO.File.WriteAllText("session.cfg", "remember_me=true"); } catch { }
                }
                else
                {
                    if (System.IO.File.Exists("session.cfg"))
                        try { System.IO.File.Delete("session.cfg"); } catch { }
                }
                OpenMainForm();
            }
            else
            {
                MessageBox.Show("Неверные учетные данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRegisterSubmit_Click(object sender, EventArgs e)
        {
            if (txtRegPassword.Text != txtRegPasswordConfirm.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            if (await DatabaseHelper.RegisterAsync(txtRegLogin.Text, txtRegName.Text, txtRegEmail.Text, txtRegPassword.Text))
            {
                MessageBox.Show("Регистрация успешна!");
                lblLoginLink_Click(this, EventArgs.Empty);
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            UserSession.UserId = null;
            UserSession.Login = "Гость";
            UserSession.Name = "Гость";
            UserSession.Email = "Гость";
            UserSession.IsAdmin = false;
            OpenMainForm();
        }
        private string GetMachineGuid()
        {
            try
            {
                return Microsoft.Win32.Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography",
                    "MachineGuid",
                    null)?.ToString();
            }
            catch
            {

                return Environment.MachineName;
            }
        }
        private void btnShowPassword_Click(object sender, EventArgs e) => txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        private void OpenMainForm() { tmrAnimation.Stop(); new MainForm().Show(); this.Hide(); }

        private async void AuthForm_Load(object sender, EventArgs e)
        {

            if (System.IO.File.Exists("session.cfg"))
            {
                string savedGuid = GetMachineGuid();


                this.Enabled = false;

                if (await DatabaseHelper.AuthenticateByGuidAsync(savedGuid))
                {
                    OpenMainForm();
                    return;
                }

                this.Enabled = true;

                try { System.IO.File.Delete("session.cfg"); } catch { }
            }
        }
        private void label9_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRegPassword.UseSystemPasswordChar = !txtRegPassword.UseSystemPasswordChar;
            txtRegPasswordConfirm.UseSystemPasswordChar = !txtRegPasswordConfirm.UseSystemPasswordChar;
        }
    }
}