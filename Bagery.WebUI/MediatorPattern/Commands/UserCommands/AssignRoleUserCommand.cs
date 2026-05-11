using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class AssignRoleUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public List<RoleAssignDto> Roles { get; set; } = new();
    }

    public class RoleAssignDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; } 
    }
}
