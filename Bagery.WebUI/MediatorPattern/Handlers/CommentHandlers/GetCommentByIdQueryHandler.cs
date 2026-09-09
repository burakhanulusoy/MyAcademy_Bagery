using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using Bagery.WebUI.MediatorPattern.Results.CommentResults;
using Bagery.WebUI.Repositories.CommentRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class GetCommentByIdQueryHandler(ICommentRepository _commentRepository)
        : IRequestHandler<GetCommentByIdQuery, GetCommentByIdQueryResult>
    {
        public async Task<GetCommentByIdQueryResult> Handle(
            GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetCommentByIdWithUserAndBlogAsync(request.Id);
            return comment.Adapt<GetCommentByIdQueryResult>();
        }
    }
}