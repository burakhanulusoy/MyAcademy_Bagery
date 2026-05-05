using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class GetCategoriesWithProductsQueryHandler(ICategoryRepository _categoryRepository) : IRequestHandler<GetCategoriesWithProductsQuery, List<GetCategoriesWithProductsQueryResult>>
    {
        public async Task<List<GetCategoriesWithProductsQueryResult>> Handle(GetCategoriesWithProductsQuery request, CancellationToken cancellationToken)
        {
            var items = await _categoryRepository.GetCategoryWithProjectsAsync();
            return items.Adapt<List<GetCategoriesWithProductsQueryResult>>();
        }
    }
}
