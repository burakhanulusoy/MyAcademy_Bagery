using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.RoleQueries;
using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.RoleHandlers
{
    public class GetRolesWithUserCountQueryHandler(RoleManager<AppRole> _roleManager,
                                                  UserManager<AppUser> _userManager) : IRequestHandler<GetRolesWithUserCountQuery, List<GetRolesWithUserCountQueryResult>>
    {
        public async Task<List<GetRolesWithUserCountQueryResult>> Handle(GetRolesWithUserCountQuery request, CancellationToken cancellationToken)
        {

            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

            var roleList = new List<GetRolesWithUserCountQueryResult>();

            foreach (var role in roles)
            {
                // Identity'nin kendi orijinal metodu ile o roldeki kullanıcıları buluyoruz
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

                roleList.Add(new GetRolesWithUserCountQueryResult
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserCount = usersInRole.Count // Kullanıcı sayısını ekliyoruz
                });
            }

            return roleList;

        }
    }
}
