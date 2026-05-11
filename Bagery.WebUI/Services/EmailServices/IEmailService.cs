namespace Bagery.WebUI.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailFor2FactorAuthentication(string email, string code, string name);
        Task SendPasswordResetLinkAsync(string email, string resetLink, string name);
    }
}
