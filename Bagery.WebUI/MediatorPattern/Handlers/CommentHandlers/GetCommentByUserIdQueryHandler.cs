using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using Bagery.WebUI.Repositories.CommentRepositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class GetCommentByUserIdQueryHandler(ICommentRepository _commentRepository,
                                                IHttpContextAccessor _httpContextAccessor,
                                                UserManager<AppUser> _userManager)
        : IRequestHandler<GetCommentByUserIdQuery, List<GetCommentByUserIdQueryResult>>
    {
        public async Task<List<GetCommentByUserIdQueryResult>> Handle(
            GetCommentByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var comments = await _commentRepository.GetCommentByUserWitBlogAsync(user.Id);
            return comments.Adapt<List<GetCommentByUserIdQueryResult>>();
        }
    }
}