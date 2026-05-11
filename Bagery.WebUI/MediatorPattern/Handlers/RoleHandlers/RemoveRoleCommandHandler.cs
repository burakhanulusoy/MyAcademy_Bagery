using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.RoleCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class RemoveRoleCommandHandler(UserManager<AppUser> _userManager,
                                          RoleManager<AppRole> _roleManager) : IRequestHandler<RemoveRoleCommand>
    {
        public async Task Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            var userInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            if(userInRole.Any())
            {
                throw new IdentityException($"Bu rolde aktif {userInRole.Count} kullanıcı bulunuyor. Silmek için önce bu kullanıcıların rolünü değiştirmelisiniz.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new IdentityException("Rol silinirken bir hata oluştu.");
            }
        }
    }
}
