using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetUserForFobiaTemplateQueryHandler(UserManager<AppUser> _userManager,
                                                     IHttpContextAccessor _http) : IRequestHandler<GetUserForFobiaTemplateQuery, GetUserForFobiaTemplateQueryResult>
    {
        public async Task<GetUserForFobiaTemplateQueryResult> Handle(GetUserForFobiaTemplateQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            return user.Adapt<GetUserForFobiaTemplateQueryResult>();



        }
    }
}
