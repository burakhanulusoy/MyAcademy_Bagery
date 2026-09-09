using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using Bagery.WebUI.Repositories.CommentRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class GetCommentsQueryHandler(ICommentRepository _commentRepository)
        : IRequestHandler<GetCommentsQuery, List<GetCommentsQueryResult>>
    {
        public async Task<List<GetCommentsQueryResult>> Handle(
            GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAllCommentWithUserAndBlogAsync();
            return comments.Adapt<List<GetCommentsQueryResult>>();
        }
    }
}