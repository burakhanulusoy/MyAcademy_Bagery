using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetUsersQueryHandler(UserManager<AppUser> _userManager) : IRequestHandler<GetUsersQuery, List<GetUsersQueryResult>>
    {
        public async Task<List<GetUsersQueryResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .ToListAsync(cancellationToken);


            return users.Adapt<List<GetUsersQueryResult>>();
          




        }
    }
}
