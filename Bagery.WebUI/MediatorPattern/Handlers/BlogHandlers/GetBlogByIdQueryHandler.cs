using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.Repositories.BlogRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class GetBlogByIdQueryHandler(IBlogRepository _blogRepository) : IRequestHandler<GetBlogByIdQuery, GetBlogByIdQueryResult>
    {
        public async Task<GetBlogByIdQueryResult> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetBlogByIdWithUser(request.Id);
            return blog.Adapt<GetBlogByIdQueryResult>();
        }
    }
}
