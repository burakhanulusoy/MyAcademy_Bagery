using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class LoginUserCommand:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
