using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.Repositories.BlogRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class GetBlogsQueryHandler(IBlogRepository _blogRepository) : IRequestHandler<GetBlogsWithUserQuery, List<GetBlogsWithUserQueryResult>>
    {
        public async Task<List<GetBlogsWithUserQueryResult>> Handle(GetBlogsWithUserQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetAllBlogsWithUserAsync();
            return blogs.Adapt<List<GetBlogsWithUserQueryResult>>();
        }
    }
}
