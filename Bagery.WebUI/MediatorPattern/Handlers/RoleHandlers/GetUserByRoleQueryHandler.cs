using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.RoleQueries;
using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class GetUserByRoleQueryHandler(UserManager<AppUser> _userManager,
                                           RoleManager<AppRole> _roleManager) : IRequestHandler<GetUserByRoleIdQuery, List<GetUserByRolIdQueryResult>>
    {
        public async Task<List<GetUserByRolIdQueryResult>> Handle(GetUserByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            if (role == null)
                return new List<GetUserByRolIdQueryResult>();

            var userInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            var result = userInRole
                .Where(user => !user.IsDeleted) 
                .Select(user => new GetUserByRolIdQueryResult
                {
                    RoleId = request.Id,
                    Users = user.Adapt<GetUserForFobiaTemplateQueryResult>()
                }).ToList();

            return result;
        }
    }
}