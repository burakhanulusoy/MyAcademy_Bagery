using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.Repositories.BlogRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class GetBlogLast4QueryHandler(IBlogRepository _blogRepository) : IRequestHandler<GetBlogLast4Query, List<GetBlogLast4QueryResult>>
    {
        public async Task<List<GetBlogLast4QueryResult>> Handle(GetBlogLast4Query request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetBlogLast4Async();
            return blogs.Adapt<List<GetBlogLast4QueryResult>>();
        }
    }
}
