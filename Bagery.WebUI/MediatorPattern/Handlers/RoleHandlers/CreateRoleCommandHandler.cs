using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.RoleCommands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class CreateRoleCommandHandler(RoleManager<AppRole> _roleManager) : IRequestHandler<CreateRoleCommand>
    {
        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleManager.RoleExistsAsync(request.Name);

            if (roleExist)
            {
                throw new IdentityException("Bu isimde bir rol zaten sistemde kayıtlı.");
            }

            var role=request.Adapt<AppRole>();

            var result = await _roleManager.CreateAsync(role);

            if(!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }




        }
    }
}
