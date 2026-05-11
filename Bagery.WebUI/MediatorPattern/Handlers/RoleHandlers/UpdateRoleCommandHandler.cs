using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.RoleCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class UpdateRoleCommandHandler(RoleManager<AppRole> _roleManager) : IRequestHandler<UpdateRoleCommand>
    {
        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            if (role.Name != request.Name)
            {
                var roleExist = await _roleManager.RoleExistsAsync(request.Name);
                if (roleExist)
                {
                    throw new IdentityException("Bu isimde bir rol zaten sistemde kayıtlı. Lütfen farklı bir isim giriniz.");
                }
            }

            role.Name = request.Name;

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }

        }
    }
}
