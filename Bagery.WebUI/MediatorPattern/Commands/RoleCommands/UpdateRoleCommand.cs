using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.RoleCommands
{
    public class UpdateRoleCommand:IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }



    }
}
