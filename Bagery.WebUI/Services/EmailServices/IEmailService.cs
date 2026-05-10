namespace Bagery.WebUI.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailFor2FactorAuthentication(string email, string code, string name);

    }
}
