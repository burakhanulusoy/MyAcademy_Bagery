using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.RoleCommands
{
    public class CreateRoleCommand:IRequest
    {
        public string Name { get; set; }
    }
}
