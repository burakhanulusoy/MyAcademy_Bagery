using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class AssignRoleUserCommandHandler(UserManager<AppUser> _userManager,
                                              IHttpContextAccessor _http) : IRequestHandler<AssignRoleUserCommand>
    {
        public async Task Handle(AssignRoleUserCommand request, CancellationToken cancellationToken)
        {
          
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            foreach(var role in request.Roles)
            {

                if(role.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);

                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }



        }
    }
}
