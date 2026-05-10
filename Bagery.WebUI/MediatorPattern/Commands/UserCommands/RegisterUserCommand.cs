using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class RegisterUserCommand:IRequest
    {

        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email  { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }



    }
}
