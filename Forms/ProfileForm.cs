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

        // ── Цвета (те же, что везде в проекте) ───────────────────────────────
        private static readonly Color AccentBlue = Color.FromArgb(45, 125, 210);
        private static readonly Color ErrorRed = Color.FromArgb(229, 57, 53);
        private static readonly Color SuccessGreen = Color.FromArgb(40, 170, 80);
        private static readonly Color FieldBg = Color.FromArgb(248, 250, 253);

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
            if (!string.IsNullOrEmpty(UserSession.AvatarUrl))
                picAvatar.ImageLocation = UserSession.AvatarUrl;
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

                lblStats.Text =
                    $"💻 Практика в IDE: {stats.LessonsCompleted} из {stats.TotalLessons} ({stats.LessonProgressPercent}%)\n\n" +
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
                MessageBox.Show("Поле 'Имя' не может быть пустым.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newEmail))
            {
                MessageBox.Show("Поле 'Email' не может быть пустым.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSaveChanges.Enabled = false;
                btnSaveChanges.Text = "⏳ Валидация Email...";
                bool isDomainReal = await DatabaseHelper.IsEmailDomainValidAsync(newEmail);

                if (!isDomainReal)
                {
                    MessageBox.Show("Указанный Email-адрес или его домен не существует.\nПожалуйста, проверьте правильность ввода.",
                        "Несуществующий Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.Equals(newEmail, UserSession.Email, StringComparison.OrdinalIgnoreCase))
                {
                    btnSaveChanges.Text = "⏳ Проверка занятости...";
                    bool emailExists = await DatabaseHelper.IsEmailExistsAsync(newEmail, UserSession.UserId);

                    if (emailExists)
                    {
                        MessageBox.Show("Этот Email уже привязан к другому аккаунту.",
                            "Email занят", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // ── ШАГ 3: Сохранение изменений ──────────────────────────────────────
                btnSaveChanges.Text = "⏳ Сохранение...";
                bool success = await DatabaseHelper.UpdateUserProfileAsync(newName, newEmail);

                if (success)
                {
                    UserSession.Name = newName;
                    UserSession.Email = newEmail;
                    lblName.Text = newName;

                    MessageBox.Show("Профиль успешно обновлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить изменения.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSaveChanges.Text = "Сохранить изменения";
                btnSaveChanges.Enabled = true;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // СМЕНА ПАРОЛЯ С ПОДТВЕРЖДЕНИЕМ EMAIL
        // ════════════════════════════════════════════════════════════════════
        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Гость — пароль менять нельзя
            if (UserSession.UserId == null)
            {
                MessageBox.Show("Смена пароля доступна только для зарегистрированных пользователей.",
                    "Недоступно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string email = UserSession.Email;

            // ── Шаг 1: Верификация email ───────────────────────────────────
            btnChangePassword.Enabled = false;
            btnChangePassword.Text = "⏳ Отправляем код…";

            using (var verifyForm = new EmailVerificationForm(email, "смены пароля"))
            {
                verifyForm.Owner = this;
                var result = verifyForm.ShowDialog(this);

                btnChangePassword.Enabled = true;
                btnChangePassword.Text = "🔑 Сменить пароль";

                if (result != DialogResult.OK || !verifyForm.Verified)
                    return; // Пользователь отменил или не прошёл верификацию
            }

            // ── Шаг 2: Форма ввода нового пароля ──────────────────────────
            ShowNewPasswordDialog();
        }

        private async void ShowNewPasswordDialog()
        {
            // ── Форма нового пароля ────────────────────────────────────────
            var prompt = new Form
            {
                Width = 400,
                Height = 310,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Новый пароль",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                BackColor = Color.White
            };

            // Заголовок
            var lblTitle = new Label
            {
                Text = "Создайте новый пароль",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 35, 55),
                AutoSize = false,
                Size = new Size(340, 26),
                Location = new Point(20, 18)
            };
            var lblSub = new Label
            {
                Text = "Email подтверждён ✅",
                Font = new Font("Segoe UI", 9F),
                ForeColor = SuccessGreen,
                AutoSize = false,
                Size = new Size(340, 18),
                Location = new Point(20, 46)
            };

            // Поле «Новый пароль»
            var lblPwd1 = new Label
            {
                Text = "Новый пароль",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 90, 110),
                AutoSize = true,
                Location = new Point(20, 80)
            };
            var txtPwd1 = new TextBox
            {
                Size = new Size(310, 26),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = FieldBg,
                UseSystemPasswordChar = true,
                Location = new Point(20, 98)
            };

            // Полоска надёжности пароля
            var pnlStrength = new Panel
            {
                Location = new Point(20, 128),
                Size = new Size(310, 4),
                BackColor = Color.FromArgb(225, 228, 235)
            };
            var pbFill = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(0, 4),
                BackColor = ErrorRed
            };
            pnlStrength.Controls.Add(pbFill);

            var lblStrength = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8F),
                AutoSize = true,
                Location = new Point(20, 136)
            };

            // Поле «Повторите пароль»
            var lblPwd2 = new Label
            {
                Text = "Повторите пароль",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 90, 110),
                AutoSize = true,
                Location = new Point(20, 158)
            };
            var txtPwd2 = new TextBox
            {
                Size = new Size(310, 26),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = FieldBg,
                UseSystemPasswordChar = true,
                Location = new Point(20, 176)
            };

            var chkShow = new CheckBox
            {
                Text = "Показать пароли",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(20, 208),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            chkShow.CheckedChanged += (s, ev) =>
            {
                txtPwd1.UseSystemPasswordChar = !chkShow.Checked;
                txtPwd2.UseSystemPasswordChar = !chkShow.Checked;
            };


            var btnSave = new Button
            {
                Text = "Сохранить пароль",
                Location = new Point(20, 235),
                Size = new Size(350, 40),
                BackColor = AccentBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false,
                DialogResult = DialogResult.OK
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 140, 225);

            txtPwd1.TextChanged += (s, ev) =>
            {
                string pwd = txtPwd1.Text;
                int score = GetPasswordStrength(pwd);

                int maxW = pnlStrength.Width;
                int fillW = pwd.Length == 0 ? 0 : maxW * (score + 1) / 5;
                pbFill.Width = Math.Min(fillW, maxW);

                Color c; string lbl;
                switch (score)
                {
                    case 0: c = ErrorRed; lbl = "Очень слабый"; break;
                    case 1: c = Color.FromArgb(230, 120, 40); lbl = "Слабый"; break;
                    case 2: c = Color.FromArgb(230, 180, 40); lbl = "Средний"; break;
                    case 3: c = Color.FromArgb(120, 190, 60); lbl = "Хороший"; break;
                    default: c = SuccessGreen; lbl = "Сильный"; break;
                }
                pbFill.BackColor = c;
                lblStrength.Text = pwd.Length == 0 ? "" : $"Сложность: {lbl}";
                lblStrength.ForeColor = c;
            };

            prompt.Controls.AddRange(new Control[]
            {
                lblTitle, lblSub,
                lblPwd1, txtPwd1, pnlStrength, lblStrength,
                lblPwd2, txtPwd2,
                chkShow, btnSave
            });
            prompt.AcceptButton = btnSave;

            if (prompt.ShowDialog(this) != DialogResult.OK)
                return;

            string newPassword = txtPwd1.Text;
            string confirmPwd = txtPwd2.Text;

            if (newPassword.Length < 8)
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPwd)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnChangePassword.Enabled = false;
            bool success = await DatabaseHelper.UpdateUserPasswordAsync(newPassword);

            if (success)
                MessageBox.Show("✅ Пароль успешно изменён!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Не удалось изменить пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            btnChangePassword.Enabled = true;
        }


        private static int GetPasswordStrength(string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return 0;
            int score = 0;
            if (pwd.Length >= 8) score++;
            if (pwd.Length >= 12) score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[a-z]") &&
                System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[A-Z]")) score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(pwd, @"\d")) score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[^a-zA-Z0-9]")) score++;
            return Math.Min(score, 4);
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
            var res = MessageBox.Show(
                "Обнулить ваш прогресс обучения?\nЭто удалит данные как по практике, так и по тестам.",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                btnReset.Enabled = false;
                lblStats.Text = "⏳ Очистка прогресса...";
                progressBar.Value = 0;

                await DatabaseHelper.ResetProfileProgressAsync();
                await LoadProfileDataAsync();

                MessageBox.Show("Прогресс успешно очищен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    await DatabaseHelper.UpdateMachineGuidAsync(UserSession.UserId.Value, null);

                if (System.IO.File.Exists("session.cfg"))
                    try { System.IO.File.Delete("session.cfg"); }
                    catch (Exception ex)
                    { System.Diagnostics.Debug.WriteLine($"[Logout Error] {ex.Message}"); }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Logout Error] {ex.Message}");
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

        private void ProfileForm_Load(object sender, EventArgs e) { }
    }
}