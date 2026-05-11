using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetUserByIdQueryHandler(UserManager<AppUser> _userManager) : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
    {
        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.Users.AsNoTracking().Where(x => x.Id == request.Id)
                                               .Include(x => x.Comments)
                                               .Include(x => x.Blogs)
                                               .FirstOrDefaultAsync();

            var roles = await _userManager.GetRolesAsync(user);
            var result = user.Adapt<GetUserByIdQueryResult>();
            result.Roles = roles;
            return result;
        }
    }
}
