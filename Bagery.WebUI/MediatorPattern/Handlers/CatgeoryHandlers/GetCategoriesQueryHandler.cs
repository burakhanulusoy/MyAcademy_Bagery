using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class GetCategoriesQueryHandler(ICategoryRepository _categoryRepository) : IRequestHandler<GetCategoriesQuery, List<GetCategoriesQueryResult>>
    {
        public async Task<List<GetCategoriesQueryResult>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var items = await _categoryRepository.GetAllAsync();
            return items.Adapt<List<GetCategoriesQueryResult>>();

        }
    }
}
