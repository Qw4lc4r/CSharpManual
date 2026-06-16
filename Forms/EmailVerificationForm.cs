using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CSharpDesctop.Services;

namespace CSharpDesctop.Forms
{
    /// <summary>
    /// Форма ввода 6-значного кода подтверждения.
    /// Используется как при регистрации, так и при смене пароля.
    /// </summary>
    public class EmailVerificationForm : Form
    {
        // ── Публичные результаты ──────────────────────────────────────────────
        public bool   Verified     { get; private set; } = false;

        // ── Внутреннее состояние ─────────────────────────────────────────────
        private readonly string _email;
        private readonly string _purpose;   // "регистрации" / "смены пароля"
        private string   _currentCode;
        private DateTime _codeExpiry;
        private int      _attemptsLeft = 3;

        // ── Таймер обратного отсчёта ─────────────────────────────────────────
        private System.Windows.Forms.Timer _countdownTimer;
        private int _secondsLeft;

        // ── Элементы UI ───────────────────────────────────────────────────────
        private Panel       _card;
        private Label       _lblTitle;
        private Label       _lblSub;
        private Label       _lblCountdown;
        private Label       _lblAttempts;
        private TextBox[]   _digitBoxes = new TextBox[6];
        private Button      _btnVerify;
        private Button      _btnResend;
        private Label       _lblStatus;

        // ── Цвета (те же, что в AuthForm) ────────────────────────────────────
        private static readonly Color AccentBlue  = Color.FromArgb(45, 125, 210);
        private static readonly Color DarkText    = Color.FromArgb(25, 35, 55);
        private static readonly Color SubText     = Color.FromArgb(130, 140, 160);
        private static readonly Color FieldBg     = Color.FromArgb(248, 250, 253);
        private static readonly Color BgColor     = Color.FromArgb(245, 248, 252);
        private static readonly Color ErrorRed    = Color.FromArgb(220, 50, 50);
        private static readonly Color SuccessGreen = Color.FromArgb(40, 170, 80);

        // ── Анимация появления ────────────────────────────────────────────────
        private System.Windows.Forms.Timer _fadeTimer;
        private float _fadeProgress = 0f;

        public EmailVerificationForm(string email, string purpose = "регистрации")
        {
            _email   = email;
            _purpose = purpose;

            SetupForm();
            BuildUI();
            WireEvents();

            this.Load += async (s, e) =>
            {
                _fadeTimer.Start();
                await SendCodeAsync();
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // НАСТРОЙКА ФОРМЫ
        // ════════════════════════════════════════════════════════════════════
        private void SetupForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            StartPosition   = FormStartPosition.CenterParent;
            ClientSize      = new Size(420, 520);
            BackColor       = BgColor;
            Text            = "Подтверждение email";
            Opacity         = 0;
        }

        // ════════════════════════════════════════════════════════════════════
        // ПОСТРОЕНИЕ UI
        // ════════════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            // ── Карточка ──────────────────────────────────────────────────
            _card = new EmailCard
            {
                Size     = new Size(370, 450),
                Location = new Point(25, 35)
            };

            // ── Иконка конверта (нарисованная) ───────────────────────────
            var iconBox = new PictureBox
            {
                Size      = new Size(64, 64),
                Location  = new Point(153, 28),
                BackColor = Color.Transparent
            };
            iconBox.Paint += (s, e) => DrawEnvelopeIcon(e.Graphics, iconBox.Size);

            // ── Заголовок ─────────────────────────────────────────────────
            _lblTitle = new Label
            {
                Text      = "Подтвердите email",
                Font      = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = DarkText,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(322, 30),
                Location  = new Point(24, 106)
            };

            // ── Подзаголовок с email ──────────────────────────────────────
            string maskedEmail = MaskEmail(_email);
            _lblSub = new Label
            {
                Text      = $"Мы отправили 6-значный код на\n{maskedEmail}",
                Font      = new Font("Segoe UI", 9.5F),
                ForeColor = SubText,
                AutoSize  = false,
                TextAlign = ContentAlignment.TopCenter,
                Size      = new Size(322, 44),
                Location  = new Point(24, 144)
            };

            // ── 6 полей для цифр ──────────────────────────────────────────
            int boxW = 42, boxH = 52, gap = 8;
            int totalW = 6 * boxW + 5 * gap;
            int startX = (370 - totalW) / 2;
            int startY = 204;

            for (int i = 0; i < 6; i++)
            {
                var tb = new TextBox
                {
                    Size        = new Size(boxW, boxH),
                    Location    = new Point(startX + i * (boxW + gap), startY),
                    Font        = new Font("Segoe UI", 20F, FontStyle.Bold),
                    TextAlign   = HorizontalAlignment.Center,
                    MaxLength   = 1,
                    BackColor   = FieldBg,
                    BorderStyle = BorderStyle.FixedSingle,
                    ForeColor   = DarkText,
                    Tag         = i
                };
                _digitBoxes[i] = tb;
            }

            // ── Статус / ошибка ───────────────────────────────────────────
            _lblStatus = new Label
            {
                Text      = "",
                Font      = new Font("Segoe UI", 9F),
                ForeColor = ErrorRed,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(322, 22),
                Location  = new Point(24, 268)
            };

            // ── Оставшиеся попытки ────────────────────────────────────────
            _lblAttempts = new Label
            {
                Text      = "Осталось попыток: 3",
                Font      = new Font("Segoe UI", 8.5F),
                ForeColor = SubText,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(322, 18),
                Location  = new Point(24, 292)
            };

            // ── Обратный отсчёт ───────────────────────────────────────────
            _lblCountdown = new Label
            {
                Text      = "Код действителен: 10:00",
                Font      = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = AccentBlue,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(322, 18),
                Location  = new Point(24, 312)
            };

            // ── Кнопка «Подтвердить» ──────────────────────────────────────
            _btnVerify = MakePrimaryButton("Подтвердить", new Point(24, 343), 322, 42);

            // ── Кнопка «Отправить повторно» ───────────────────────────────
            _btnResend = new Button
            {
                Text      = "Отправить код повторно",
                Location  = new Point(24, 397),
                Size      = new Size(322, 34),
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9F, FontStyle.Underline),
                ForeColor = AccentBlue,
                BackColor = Color.Transparent,
                Cursor    = Cursors.Hand,
                Enabled   = false
            };
            _btnResend.FlatAppearance.BorderSize = 0;
            _btnResend.FlatAppearance.MouseOverBackColor = Color.Transparent;

            // ── Сборка карточки ───────────────────────────────────────────
            _card.Controls.Add(iconBox);
            _card.Controls.Add(_lblTitle);
            _card.Controls.Add(_lblSub);
            foreach (var tb in _digitBoxes) _card.Controls.Add(tb);
            _card.Controls.Add(_lblStatus);
            _card.Controls.Add(_lblAttempts);
            _card.Controls.Add(_lblCountdown);
            _card.Controls.Add(_btnVerify);
            _card.Controls.Add(_btnResend);

            // ── Фоновая сетка точек (как в ProfileForm) ───────────────────
            this.Paint += (s, e) => DrawDotGrid(e.Graphics);

            Controls.Add(_card);
        }

        // ════════════════════════════════════════════════════════════════════
        // СОБЫТИЯ
        // ════════════════════════════════════════════════════════════════════
        private void WireEvents()
        {
            // Логика ввода цифр: переход к следующему полю, Backspace → предыдущее
            for (int i = 0; i < 6; i++)
            {
                int idx = i;
                _digitBoxes[i].KeyPress += (s, e) =>
                {
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                        return;
                    }
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        if (idx > 0 && string.IsNullOrEmpty(_digitBoxes[idx].Text))
                        {
                            _digitBoxes[idx - 1].Focus();
                            _digitBoxes[idx - 1].Clear();
                        }
                    }
                };
                _digitBoxes[i].TextChanged += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(_digitBoxes[idx].Text) && idx < 5)
                        _digitBoxes[idx + 1].Focus();

                    // Подсветка заполненного поля
                    bool filled = !string.IsNullOrEmpty(_digitBoxes[idx].Text);
                    _digitBoxes[idx].BackColor = filled ? Color.FromArgb(236, 244, 255) : FieldBg;
                    _digitBoxes[idx].ForeColor = filled ? AccentBlue : DarkText;

                    ClearStatus();
                    AutoVerifyIfComplete();
                };
                _digitBoxes[i].KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Back && string.IsNullOrEmpty(_digitBoxes[idx].Text) && idx > 0)
                    {
                        _digitBoxes[idx - 1].Focus();
                        _digitBoxes[idx - 1].Clear();
                    }
                };
                _digitBoxes[i].GotFocus  += (s, e) => HighlightBox(_digitBoxes[idx], true);
                _digitBoxes[i].LostFocus += (s, e) => HighlightBox(_digitBoxes[idx], false);
            }

            _btnVerify.Click  += (s, e) => VerifyCode();
            _btnResend.Click  += async (s, e) => await ResendCodeAsync();

            // Таймер обратного отсчёта
            _countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _countdownTimer.Tick += CountdownTimer_Tick;

            // Таймер плавного появления
            _fadeTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _fadeTimer.Tick += (s, e) =>
            {
                _fadeProgress += 0.07f;
                if (_fadeProgress >= 1f) { _fadeProgress = 1f; _fadeTimer.Stop(); }
                Opacity = EaseOutCubic(_fadeProgress);
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // ЛОГИКА КОДА
        // ════════════════════════════════════════════════════════════════════
        private async System.Threading.Tasks.Task SendCodeAsync()
        {
            _currentCode = EmailVerificationService.GenerateCode();
            _codeExpiry  = DateTime.Now.AddMinutes(10);
            _secondsLeft = 600;

            SetStatus("⏳ Отправляем код…", SubText);
            _btnVerify.Enabled = false;
            _btnResend.Enabled = false;

            bool sent = await EmailVerificationService.SendVerificationCodeAsync(_email, _currentCode, _purpose);

            if (sent)
            {
                SetStatus("✅ Код отправлен!", SuccessGreen);
                _btnVerify.Enabled = true;
                _countdownTimer.Start();

                // Кнопка «Отправить повторно» — доступна через 60 с
                var resendTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
                resendTimer.Tick += (s, e) => { _btnResend.Enabled = true; resendTimer.Stop(); };
                resendTimer.Start();
            }
            else
            {
                SetStatus("❌ Не удалось отправить письмо.\nПроверьте настройки SMTP.", ErrorRed);
                _btnVerify.Enabled = true;
            }
        }

        private async System.Threading.Tasks.Task ResendCodeAsync()
        {
            _btnResend.Enabled = false;
            _countdownTimer.Stop();
            ClearDigits();
            await SendCodeAsync();
        }

        private void VerifyCode()
        {
            if (_attemptsLeft <= 0) return;

            string entered = "";
            foreach (var tb in _digitBoxes) entered += tb.Text;

            if (entered.Length < 6)
            {
                SetStatus("Введите все 6 цифр кода.", ErrorRed);
                ShakeCard();
                return;
            }

            if (DateTime.Now > _codeExpiry)
            {
                SetStatus("Код истёк. Запросите новый.", ErrorRed);
                ClearDigits();
                return;
            }

            if (entered == _currentCode)
            {
                _countdownTimer.Stop();
                Verified = true;
                SetStatus("✅ Код подтверждён!", SuccessGreen);

                // Анимация успеха — кратная задержка перед закрытием
                var closeTimer = new System.Windows.Forms.Timer { Interval = 800 };
                closeTimer.Tick += (s, e) => { closeTimer.Stop(); DialogResult = DialogResult.OK; Close(); };
                closeTimer.Start();
            }
            else
            {
                _attemptsLeft--;
                UpdateAttemptsLabel();

                if (_attemptsLeft <= 0)
                {
                    SetStatus("Попытки исчерпаны. Запросите новый код.", ErrorRed);
                    _btnVerify.Enabled = false;
                    _btnResend.Enabled = true;
                    ClearDigits();
                }
                else
                {
                    SetStatus($"Неверный код. Осталось попыток: {_attemptsLeft}", ErrorRed);
                    ShakeCard();
                    HighlightAllBoxesError();
                }
            }
        }

        private void AutoVerifyIfComplete()
        {
            foreach (var tb in _digitBoxes)
                if (string.IsNullOrEmpty(tb.Text)) return;

            // Авто-верификация с небольшой задержкой
            var t = new System.Windows.Forms.Timer { Interval = 200 };
            t.Tick += (s, e) => { t.Stop(); VerifyCode(); };
            t.Start();
        }

        // ════════════════════════════════════════════════════════════════════
        // ТАЙМЕР
        // ════════════════════════════════════════════════════════════════════
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            _secondsLeft--;
            int m = _secondsLeft / 60, s = _secondsLeft % 60;

            if (_secondsLeft <= 0)
            {
                _countdownTimer.Stop();
                _lblCountdown.Text     = "Код истёк";
                _lblCountdown.ForeColor = ErrorRed;
                _btnVerify.Enabled     = false;
                _btnResend.Enabled     = true;
            }
            else
            {
                _lblCountdown.Text = $"Код действителен: {m:D2}:{s:D2}";
                _lblCountdown.ForeColor = _secondsLeft <= 60 ? ErrorRed : AccentBlue;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        // ════════════════════════════════════════════════════════════════════
        private void SetStatus(string text, Color color)
        {
            _lblStatus.Text      = text;
            _lblStatus.ForeColor = color;
        }

        private void ClearStatus() => _lblStatus.Text = "";

        private void UpdateAttemptsLabel() =>
            _lblAttempts.Text = $"Осталось попыток: {_attemptsLeft}";

        private void ClearDigits()
        {
            foreach (var tb in _digitBoxes)
            {
                tb.Text      = "";
                tb.BackColor = FieldBg;
                tb.ForeColor = DarkText;
            }
            _digitBoxes[0].Focus();
        }

        private void HighlightBox(TextBox tb, bool focused)
        {
            if (string.IsNullOrEmpty(tb.Text))
                tb.BackColor = focused ? Color.FromArgb(230, 240, 255) : FieldBg;
        }

        private void HighlightAllBoxesError()
        {
            foreach (var tb in _digitBoxes)
                tb.BackColor = Color.FromArgb(255, 235, 235);

            var t = new System.Windows.Forms.Timer { Interval = 600 };
            t.Tick += (s, e) =>
            {
                t.Stop();
                ClearDigits();
            };
            t.Start();
        }

        // Анимация «тряски» карточки при ошибке
        private void ShakeCard()
        {
            Point orig = _card.Location;
            int[] offsets = { -6, 6, -5, 5, -3, 3, -1, 1, 0 };
            int   step = 0;
            var   t = new System.Windows.Forms.Timer { Interval = 30 };
            t.Tick += (s, e) =>
            {
                if (step >= offsets.Length) { t.Stop(); _card.Location = orig; return; }
                _card.Location = new Point(orig.X + offsets[step++], orig.Y);
            };
            t.Start();
        }

        // Маскирует email: example@mail.ru → ex****@mail.ru
        private static string MaskEmail(string email)
        {
            int at = email.IndexOf('@');
            if (at <= 2) return email;
            return email[..2] + new string('*', at - 2) + email[at..];
        }

        private static float EaseOutCubic(float t) => 1f - (float)Math.Pow(1f - t, 3);

        // ── Отрисовка конверта ────────────────────────────────────────────
        private void DrawEnvelopeIcon(Graphics g, Size sz)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = sz.Width, h = sz.Height;
            int pad = 4;

            // Фон-круг
            using (var brush = new SolidBrush(Color.FromArgb(230, 241, 255)))
                g.FillEllipse(brush, 0, 0, w, h);

            // Тело конверта
            var envRect = new Rectangle(pad + 4, pad + 12, w - 2 * pad - 8, h - 2 * pad - 16);
            using (var brush = new SolidBrush(Color.White))
                g.FillRoundedRect(brush, envRect, 4);
            using (var pen = new Pen(AccentBlue, 1.5f))
                g.DrawRoundedRect(pen, envRect, 4);

            // Диагонали «конверта»
            Point mid = new Point(envRect.Left + envRect.Width / 2, envRect.Top + envRect.Height / 2 - 2);
            int midX = envRect.Left + envRect.Width / 2;
            int midY = envRect.Top + envRect.Height / 2 - 2;
            using (var pen = new Pen(AccentBlue, 1.5f))
            {

                g.DrawLine(pen, envRect.Left, envRect.Top, midX, midY);
                g.DrawLine(pen, envRect.Right, envRect.Top, midX, midY);
            }
        }

        // ── Фоновая сетка точек ───────────────────────────────────────────
        private void DrawDotGrid(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int step = 24;
            using (var brush = new SolidBrush(Color.FromArgb(14, 45, 125, 210)))
                for (int x = 8; x < Width; x += step)
                    for (int y = 8; y < Height; y += step)
                        g.FillEllipse(brush, x, y, 2, 2);
        }

        protected override void Dispose(bool disposing)
        {
            _countdownTimer?.Dispose();
            _fadeTimer?.Dispose();
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;

        // Кнопка-стиль из AuthForm
        private static Button MakePrimaryButton(string text, Point loc, int width, int height = 40)
        {
            var btn = new Button
            {
                Text      = text,
                Location  = loc,
                Size      = new Size(width, height),
                BackColor = AccentBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor    = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor  = Color.FromArgb(60, 140, 225);
            btn.FlatAppearance.MouseDownBackColor  = Color.FromArgb(30, 100, 185);
            return btn;
        }
    }

    // ════════════════════════════════════════════════════════════════════════
    // КАРТОЧКА ВЕРИФИКАЦИИ (та же, что RoundedCard в AuthForm)
    // ════════════════════════════════════════════════════════════════════════
    internal class EmailCard : Panel
    {
        public EmailCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs e) =>
            e.Graphics.Clear(Parent?.BackColor ?? Color.FromArgb(245, 248, 252));

        protected override void OnPaint(PaintEventArgs e)
        {
            var g    = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(2, 2, Width - 5, Height - 5);

            // Тень
            for (int i = 4; i >= 1; i--)
            {
                var sr = new Rectangle(rect.X + i, rect.Y + i, rect.Width, rect.Height);
                using var sp = RoundPath(sr, 16);
                using var sb = new SolidBrush(Color.FromArgb(10 + i * 4, 60, 80, 150));
                g.FillPath(sb, sp);
            }
            // Заливка
            using (var path = RoundPath(rect, 16))
            using (var brush = new SolidBrush(Color.White))
                g.FillPath(brush, path);
            // Рамка
            using (var path = RoundPath(rect, 16))
            using (var pen  = new Pen(Color.FromArgb(220, 225, 235), 1f))
                g.DrawPath(pen, path);
        }

        private static GraphicsPath RoundPath(Rectangle r, int radius)
        {
            int d    = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new GraphicsPath();
            path.AddArc(r.X,          r.Y,          d, d, 180, 90);
            path.AddArc(r.Right - d,  r.Y,          d, d, 270, 90);
            path.AddArc(r.Right - d,  r.Bottom - d, d, d, 0,   90);
            path.AddArc(r.X,          r.Bottom - d, d, d, 90,  90);
            path.CloseFigure();
            return path;
        }
    }

    // ════════════════════════════════════════════════════════════════════════
    // РАСШИРЕНИЯ Graphics для скруглённых прямоугольников
    // ════════════════════════════════════════════════════════════════════════
    internal static class GraphicsExtensions
    {
        public static void FillRoundedRect(this Graphics g, Brush brush, Rectangle r, int radius)
        {
            using var path = BuildPath(r, radius);
            g.FillPath(brush, path);
        }
        public static void DrawRoundedRect(this Graphics g, Pen pen, Rectangle r, int radius)
        {
            using var path = BuildPath(r, radius);
            g.DrawPath(pen, path);
        }
        private static GraphicsPath BuildPath(Rectangle r, int radius)
        {
            int d    = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new GraphicsPath();
            path.AddArc(r.X,          r.Y,          d, d, 180, 90);
            path.AddArc(r.Right - d,  r.Y,          d, d, 270, 90);
            path.AddArc(r.Right - d,  r.Bottom - d, d, d, 0,   90);
            path.AddArc(r.X,          r.Bottom - d, d, d, 90,  90);
            path.CloseFigure();
            return path;
        }
    }
}
