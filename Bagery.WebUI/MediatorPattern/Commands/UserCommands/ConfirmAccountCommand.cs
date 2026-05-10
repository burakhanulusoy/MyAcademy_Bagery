using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class ConfirmAccountCommand:IRequest
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
    }
}
