using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CSharpDesctop.Services
{
    /// <summary>
    /// Сервис для отправки писем с кодом подтверждения.
    /// Настройте SMTP_HOST, SMTP_PORT, SMTP_USER и SMTP_PASS под ваш почтовый сервер.
    /// </summary>
    public static class EmailVerificationService
    {
        // ─── НАСТРОЙКИ SMTP ─────────────────────────────────────────────────────
        private const string SMTP_HOST = "smtp.gmail.com";
        private const int SMTP_PORT = 587;              // Порт для STARTTLS
        private const bool SMTP_SSL = true;             // ОБЯЗАТЕЛЬНО true для Gmail!
        private const string SMTP_USER = "qwertyuiopqwertyuiup456@gmail.com";
        private const string SMTP_PASS = "mavvgrkvadlqqdpi";
        private const string FROM_NAME = "C# Manual";
        // ────────────────────────────────────────────────────────────────────────

        private static readonly Random _rng = new Random();

        /// <summary>Генерирует случайный 6-значный код.</summary>
        public static string GenerateCode() => _rng.Next(100000, 999999).ToString();

        /// <summary>
        /// Отправляет письмо с кодом подтверждения на указанный email.
        /// Возвращает true при успехе.
        /// </summary>
        public static async Task<bool> SendVerificationCodeAsync(string toEmail, string code, string purpose = "регистрации")
        {
            try
            {

                using var client = new SmtpClient(SMTP_HOST, SMTP_PORT)
                {
                    EnableSsl            = SMTP_SSL,
                    DeliveryMethod       = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials          = new NetworkCredential(SMTP_USER, SMTP_PASS)
                };

                // Если STARTTLS — нужно явно включить TLS
                if (!SMTP_SSL && SMTP_PORT == 587)
                    System.Net.ServicePointManager.SecurityProtocol =
                        System.Net.SecurityProtocolType.Tls12;

                string subject = $"C# Manual — Код {purpose}";
                string body    = BuildEmailBody(code, purpose);

                var msg = new MailMessage
                {
                    From       = new MailAddress(SMTP_USER, FROM_NAME),
                    Subject    = subject,
                    Body       = body,
                    IsBodyHtml = true
                };
                msg.To.Add(toEmail);

                await client.SendMailAsync(msg);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Email Error] {ex.Message}");
                return false;
            }
        }

        private static string BuildEmailBody(string code, string purpose)
        {
            return $@"
<!DOCTYPE html>
<html lang='ru'>
<head><meta charset='UTF-8'></head>
<body style='margin:0;padding:0;background:#f4f7fb;font-family:Segoe UI,Arial,sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' style='padding:40px 0;'>
    <tr><td align='center'>
      <table width='420' cellpadding='0' cellspacing='0'
             style='background:#fff;border-radius:16px;border:1px solid #e2e8f0;
                    box-shadow:0 4px 24px rgba(45,125,210,0.08);overflow:hidden;'>

        <!-- Заголовок с градиентом -->
        <tr>
          <td style='background:linear-gradient(135deg,#1a3c8a,#2d7dd2);
                     padding:32px 40px;text-align:center;'>
            <div style='font-size:28px;font-weight:800;color:#fff;letter-spacing:-0.5px;'>
              C# Manual
            </div>
            <div style='font-size:13px;color:rgba(200,220,255,0.85);margin-top:4px;'>
              Электронный учебный справочник
            </div>
          </td>
        </tr>

        <!-- Тело -->
        <tr>
          <td style='padding:36px 40px;'>
            <p style='margin:0 0 8px;font-size:18px;font-weight:700;color:#19233a;'>
              Подтверждение {purpose}
            </p>
            <p style='margin:0 0 28px;font-size:14px;color:#64748b;line-height:1.6;'>
              Введите этот код в приложении. Он действителен&nbsp;<strong>10&nbsp;минут</strong>.
            </p>

            <!-- Блок с кодом -->
            <div style='background:#f0f6ff;border:2px dashed #2d7dd2;border-radius:12px;
                        padding:20px;text-align:center;margin-bottom:28px;'>
              <span style='font-size:40px;font-weight:800;letter-spacing:12px;
                           color:#2d7dd2;font-family:Consolas,monospace;'>
                {code}
              </span>
            </div>

            <p style='margin:0;font-size:12px;color:#94a3b8;line-height:1.6;'>
              Если вы не запрашивали этот код — просто проигнорируйте письмо.<br>
              Ваши данные в безопасности.
            </p>
          </td>
        </tr>

        <!-- Подвал -->
        <tr>
          <td style='background:#f8fafc;padding:20px 40px;border-top:1px solid #e2e8f0;
                     text-align:center;font-size:11px;color:#94a3b8;'>
            © 2026 C# Manual. Выпускная квалификационная работа.
          </td>
        </tr>
      </table>
    </td></tr>
  </table>
</body>
</html>";
        }
    }
}
