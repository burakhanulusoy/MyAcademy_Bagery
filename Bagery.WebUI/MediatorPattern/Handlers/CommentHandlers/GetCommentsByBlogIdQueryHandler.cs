using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using Bagery.WebUI.Repositories.CommentRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class GetCommentsByBlogIdQueryHandler(ICommentRepository _commentRepository)
        : IRequestHandler<GetCommentsByBlogIdQuery, List<GetCommentsByBlogIdQueryResult>>
    {
        public async Task<List<GetCommentsByBlogIdQueryResult>> Handle(
            GetCommentsByBlogIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsByBlogIdAsync(request.BlogId);
            return comments.Adapt<List<GetCommentsByBlogIdQueryResult>>();
        }
    }
}