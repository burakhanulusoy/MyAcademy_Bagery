
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

        public async Task SendPasswordResetLinkAsync(string email, string resetLink, string name)
        {
            var emailConfirmCode = configuration["Email:Code"];
            var adminEmail = configuration["Email:Admin"];

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Bagery Admin", adminEmail));
            mimeMessage.To.Add(new MailboxAddress(name, email));
            mimeMessage.Subject = "Bagery - Şifre Sıfırlama Talebi";

            var bodyBuilder = new BodyBuilder();

            // Şık bir buton içeren HTML tasarımı
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h3 style='color: #0056b3;'>Merhaba {name},</h3>
                    <p>Bagery platformundaki hesabının şifresini sıfırlamak için bir talep aldık.</p>
                    <p>Yeni şifreni belirlemek için aşağıdaki butona tıklayabilirsin:</p>
                    <div style='text-align: center; margin: 30px 0;'>
                        <a href='{resetLink}' style='background-color: #0056b3; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;'>Şifremi Sıfırla</a>
                    </div>
                    <p>Eğer bu talebi sen yapmadıysan, bu e-postayı görmezden gelebilirsin. Hesabın güvendedir.</p>
                    <br>
                    <p>İyi günler dileriz,<br><strong>Bagery Ekibi</strong></p>
                </div>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(adminEmail, emailConfirmCode);
                await client.SendAsync(mimeMessage);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendPromotionAsync(List<string> emails, string description, string code)
        {
            if (emails == null || !emails.Any()) return; // Gönderilecek mail yoksa işlemi iptal et

            var emailConfirmCode = configuration["Email:Code"];
            var adminEmail = configuration["Email:Admin"];

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Bagery Kampanya", adminEmail));

            // Tüm mailleri BCC (Gizli Karbon Kopya) olarak ekliyoruz
            foreach (var email in emails)
            {
                mimeMessage.Bcc.Add(new MailboxAddress("", email));
            }

            mimeMessage.Subject = "Bagery'den Yeni Bir Fırsat Var!";

            var bodyBuilder = new BodyBuilder();

            // Promosyon için şık bir HTML şablonu
            bodyBuilder.HtmlBody = $@"
        <div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
            <h2 style='color: #28a745;'>Yeni Kampanyamızı Kaçırma!</h2>
            <p style='font-size: 16px; line-height: 1.5;'>{description}</p>
            
            <div style='background-color: #f8f9fa; padding: 20px; text-align: center; border-radius: 8px; margin: 25px 0; border: 1px dashed #28a745;'>
                <p style='margin: 0; font-size: 14px; color: #666;'>İndirim Kodun</p>
                <h1 style='color: #d9534f; letter-spacing: 3px; margin: 10px 0;'>{code}</h1>
            </div>
            
            <p>Bizi tercih ettiğiniz için teşekkür ederiz.<br><strong>Bagery Ekibi</strong></p>
        </div>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
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
