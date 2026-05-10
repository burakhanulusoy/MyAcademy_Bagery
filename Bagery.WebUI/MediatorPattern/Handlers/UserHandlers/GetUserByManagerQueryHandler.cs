using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetUserByManagerQueryHandler(UserManager<AppUser> _userManager,
                                               IHttpContextAccessor _http) : IRequestHandler<GetUserByManagerQuery, GetUserByManagerForEditQueryResult>
    {
        public async Task<GetUserByManagerForEditQueryResult> Handle(GetUserByManagerQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            var mappedUser=user.Adapt<GetUserByManagerForEditQueryResult>();

            return mappedUser;



        }
    }
}
