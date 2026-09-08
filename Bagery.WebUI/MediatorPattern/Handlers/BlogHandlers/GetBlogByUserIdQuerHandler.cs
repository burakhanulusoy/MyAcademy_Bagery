using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.Repositories.BlogRepositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class GetBlogByUserIdQuerHandler(IBlogRepository _blogRepository,
                                           IHttpContextAccessor httpContextAccessor,
                                           UserManager<AppUser> userManager) : IRequestHandler<GetBlogByUserIdQuery, List<GetBlogByUserIdQueryResult>>
    {
        public async Task<List<GetBlogByUserIdQueryResult>> Handle(GetBlogByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var blog = await _blogRepository.GetBlogByUserIdAsync(user.Id);
            return blog.Adapt<List<GetBlogByUserIdQueryResult>>();

        }
    }
}
