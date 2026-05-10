
using MailKit.Net.Smtp;
using MimeKit;

namespace Bagery.WebUI.Services.EmailServices
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailFor2FactorAuthentication(string email, string code, string name)
        {
            // user secrets veya appsettings üzerinden verileri çekiyoruz
            var emailConfirmCode = configuration["Email:Code"];
            var adminEmail = configuration["Email:Admin"];

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Bagery Admin", adminEmail));
            mimeMessage.To.Add(new MailboxAddress(name, email)); // Alıcı adı olarak kullanıcının kendi adını(name) verdik
            mimeMessage.Subject = "Bagery - İki Adımlı Doğrulama Kodu";

            var bodyBuilder = new BodyBuilder();

            // Mail içeriğini HTML olarak tasarlayalım, göze daha hoş gelsin
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h3>Merhaba {name},</h3>
                    <p>Bagery platformundaki işlemini tamamlamak için doğrulama kodun aşağıdadır:</p>
                    <h2 style='color: #d9534f; letter-spacing: 2px;'>{code}</h2>
                    <p>Güvenliğin için bu kodu lütfen kimseyle paylaşma.</p>
                    <br>
                    <p>İyi günler dileriz,<br><strong>Bagery Ekibi</strong></p>
                </div>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                // Bağlantı ayarları eski kodundaki gibi standart Gmail SMTP
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(adminEmail, emailConfirmCode);
                await client.SendAsync(mimeMessage);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
