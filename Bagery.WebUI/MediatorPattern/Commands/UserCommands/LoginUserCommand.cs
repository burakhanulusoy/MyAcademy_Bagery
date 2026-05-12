using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class LoginUserCommand:IRequest<List<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
