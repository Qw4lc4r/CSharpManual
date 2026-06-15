using CSharpDesctop.Services;
using CSharpDesctop.Models;
using CSharpDesctop.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpDesctop.Forms
{
    public partial class ProfileForm : Form
    {
        private string selectedAvatarPath = null;

        
        private System.Windows.Forms.Timer _fadeInTimer;
        private int _animationOffsetY = 20; 
        private float _appearanceProgress = 0f;

        
        private RoundedPanel _nameWrapper;
        private RoundedPanel _emailWrapper;

        public ProfileForm()
        {
            InitializeComponent();

            
            pnlHeaderCard.HoverHighlight = true;
            pnlStatsCard.HoverHighlight = true;

            
            AvatarHelper.MakeCircular(picAvatar);
            picAvatar.Resize += (s, e) => AvatarHelper.MakeCircular(picAvatar);

            
            StyleInputFields();

            
            lblName.Text = string.IsNullOrWhiteSpace(UserSession.Name) ? "Профиль" : UserSession.Name;
            lblUserLogin.Text = $"Логин: {UserSession.Login}";
            txtEditName.Text = UserSession.Name;
            txtEditEmail.Text = UserSession.Email;

            LoadUserAvatar();

            btnReset.Enabled = false;
            btnSaveChanges.Enabled = false;

            
            PrepareElementsForAnimation();

            
            _fadeInTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _fadeInTimer.Tick += FadeInTimer_Tick;

            this.Load += (s, e) => _fadeInTimer.Start();

            _ = LoadProfileDataAsync();
        }

        private void StyleInputFields()
        {
            
            _nameWrapper = InputStyler.WrapTextBox(txtEditName, 10);
            _emailWrapper = InputStyler.WrapTextBox(txtEditEmail, 10);

            
            pnlHeaderCard.Controls.Add(_nameWrapper);
            pnlHeaderCard.Controls.Add(_emailWrapper);

            
            _nameWrapper.Location = new Point(20, 155);
            _emailWrapper.Location = new Point(20, 195);

            
            _nameWrapper.Width = pnlHeaderCard.Width - 40;
            _emailWrapper.Width = pnlHeaderCard.Width - 40;
        }

        private void PrepareElementsForAnimation()
        {
            
            pnlHeaderCard.Top += _animationOffsetY;
            pnlStatsCard.Top += _animationOffsetY;

            btnAboutApp.Top += _animationOffsetY;
            btnChangePassword.Top += _animationOffsetY;
            btnReset.Top += _animationOffsetY;

            
            Opacity = 0;
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            _appearanceProgress += 0.06f; 
            if (_appearanceProgress >= 1f)
            {
                _appearanceProgress = 1f;
                _fadeInTimer.Stop();
            }

            
            Opacity = _appearanceProgress;

            
            int currentOffset = (int)(_animationOffsetY * (1f - EaseOutCubic(_appearanceProgress)));

            pnlHeaderCard.Top = 18 + currentOffset;
            pnlStatsCard.Top = 340 + currentOffset;

            btnAboutApp.Top = 566 + currentOffset;
            btnChangePassword.Top = 614 + currentOffset;
            btnReset.Top = 664 + currentOffset;

            Invalidate();
        }

        
        private static float EaseOutCubic(float t) => 1f - (float)Math.Pow(1f - t, 3);

        private void LoadUserAvatar()
        {
            MessageBox.Show(UserSession.AvatarUrl);
            
            if (!string.IsNullOrEmpty(UserSession.AvatarUrl))
            {
                picAvatar.ImageLocation = UserSession.AvatarUrl;
            }
            else
            {
                
                picAvatar.Image = null;
                picAvatar.BackColor = Color.FromArgb(214, 232, 250);
            }
        }

        private async Task LoadProfileDataAsync()
        {
            try
            {
                UserStatsModel stats = await DatabaseHelper.GetUserStatsAsync();
                if (stats == null) return;

                
                progressBar.Value = Math.Max(0, Math.Min(100, stats.OverallProgressPercent));

                lblStats.Text = $"💻 Практика в IDE: {stats.LessonsCompleted} из {stats.TotalLessons} ({stats.LessonProgressPercent}%)\n\n" +
                                $"📱 Тесты (Mobile): {stats.TestsPassed} из {stats.TotalTests} ({stats.TestProgressPercent}%)\n\n" +
                                $"🏆 Общий прогресс курса: {stats.OverallProgressPercent}%";
            }
            catch (Exception ex)
            {
                lblStats.Text = "❌ Ошибка загрузки статистики";
                System.Diagnostics.Debug.WriteLine($"Ошибка профиля: {ex.Message}");
            }
            finally
            {
                btnReset.Enabled = true;
                btnSaveChanges.Enabled = true;
            }
        }

        private async void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedAvatarPath = ofd.FileName;

                    btnChangeAvatar.Enabled = false;
                    btnChangeAvatar.Text = "⏳...";

                    string remoteUrl = await DatabaseHelper.UploadAvatarAsync(selectedAvatarPath);

                    if (!string.IsNullOrEmpty(remoteUrl))
                    {
                        picAvatar.ImageLocation = remoteUrl;
                        MessageBox.Show("Аватар успешно обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить изображение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    btnChangeAvatar.Text = "📷 Фото";
                    btnChangeAvatar.Enabled = true;
                }
            }
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string newName = txtEditName.Text.Trim();
            string newEmail = txtEditEmail.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Поле 'Имя' не может быть пустым.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSaveChanges.Enabled = false;
            bool success = await DatabaseHelper.UpdateUserProfileAsync(newName, newEmail);

            if (success)
            {
                UserSession.Name = newName;
                lblName.Text = newName;
                MessageBox.Show("Профиль успешно обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось сохранить изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnSaveChanges.Enabled = true;
        }

        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 190,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Смена пароля",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                BackColor = Color.White
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Введите новый пароль:", Width = 250, Font = new Font("Segoe UI", 9.5F) };
            TextBox textBox = new TextBox() { Width = 290, PasswordChar = '*', Font = new Font("Segoe UI", 10F) };

            
            RoundedPanel inputWrapper = InputStyler.WrapTextBox(textBox, 8);
            inputWrapper.Location = new Point(20, 45);

            
            CheckBox chkShowPassword = new CheckBox()
            {
                Text = "Показать пароль",
                Left = 20,
                Top = 102, 
                Width = 150,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Cursor = Cursors.Hand
            };

            
            chkShowPassword.CheckedChanged += (s, ev) =>
            {
                textBox.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
            };

            RoundedButton confirmation = new RoundedButton()
            {
                Text = "ОК",
                Left = 210,
                Width = 100,
                Height = 35,
                Top = 95,
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(45, 125, 210),
                ForeColor = Color.White,
                CornerRadius = 8
            };

            prompt.Controls.Add(inputWrapper);
            prompt.Controls.Add(chkShowPassword); 
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string newPassword = textBox.Text.Trim();
                if (newPassword.Length < 4)
                {
                    MessageBox.Show("Пароль слишком короткий (минимум 4 символа)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await DatabaseHelper.UpdateUserPasswordAsync(newPassword);
                if (success)
                    MessageBox.Show("Пароль успешно изменен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Не удалось изменить пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAboutApp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "📚 Электронный учебный справочник 'C# Manual'\n" +
                "Разработано в рамках выпускной квалификационной (дипломной) работы.\n\n" +
                "Версия клиента: 1.0.0\n" +
                "Платформа синхронизации данных: Supabase (PostgreSQL)\n\n" +
                "© 2026 Все права защищены.",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Обнулить ваш прогресс обучения?\nЭто удалит данные как по практике, так и по тестам.",
                                      "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                btnReset.Enabled = false;
                lblStats.Text = "⏳ Очистка прогресса...";
                progressBar.Value = 0;

                await DatabaseHelper.ResetProfileProgressAsync();
                await LoadProfileDataAsync();

                MessageBox.Show("Прогресс успешно очищен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fadeInTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private async void roundedButton1_Click(object sender, EventArgs e)
        {
            
            if (sender is Button btn) btn.Enabled = false;

            try
            {
                
                if (UserSession.UserId.HasValue)
                {
                    
                    await DatabaseHelper.UpdateMachineGuidAsync(UserSession.UserId.Value, null);
                }

                
                if (System.IO.File.Exists("session.cfg"))
                {
                    try
                    {
                        System.IO.File.Delete("session.cfg");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Logout Error] Не удалось удалить локальный файл: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Logout Error] Ошибка при деавторизации: {ex.Message}");
            }

            
            UserSession.UserId = null;
            UserSession.Login = "Гость";
            UserSession.Name = "Гость";
            UserSession.Email = "Гость";
            UserSession.IsAdmin = false;
            UserSession.AvatarUrl = "";

            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {

        }
    }
}