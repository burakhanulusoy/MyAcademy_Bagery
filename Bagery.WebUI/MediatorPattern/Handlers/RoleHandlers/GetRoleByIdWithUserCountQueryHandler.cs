using AspNetCoreGeneratedDocument;
using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.RoleQueries;
using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class GetRoleByIdWithUserCountQueryHandler(UserManager<AppUser> _userManager,
                                                      RoleManager<AppRole> _roleManager) : IRequestHandler<GetRoleByIdWithUserCountQuery, GetRoleByIdWithUserCountQueryResult>
    {
        public async Task<GetRoleByIdWithUserCountQueryResult> Handle(GetRoleByIdWithUserCountQuery request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            return new GetRoleByIdWithUserCountQueryResult
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            
        }
    }
}
