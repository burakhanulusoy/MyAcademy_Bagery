using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetUserRolesQueryHandler(UserManager<AppUser> _userManager,
                                          IHttpContextAccessor _http,
                                          RoleManager<AppRole> _roleManager) : IRequestHandler<GetUserRolesQuery, List<GetUserRoleQueryResult>>
    {
        public async Task<List<GetUserRoleQueryResult>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

            var userRoles = await _userManager.GetRolesAsync(user);

            var assignRoleList = new List<GetUserRoleQueryResult>();

            foreach (var role in roles)
            {
                assignRoleList.Add(new GetUserRoleQueryResult
                {

                    RoleId = role.Id,
                    RoleName = role.Name,
                    Id = user.Id,
                    RoleExist = userRoles.Contains(role.Name)


                });


            }

            return assignRoleList;




        }
    }
}
