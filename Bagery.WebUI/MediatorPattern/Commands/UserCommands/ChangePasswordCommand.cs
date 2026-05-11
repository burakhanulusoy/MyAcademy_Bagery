using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class ChangePasswordCommand:IRequest
    {
        public string NowPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }
    }
}
