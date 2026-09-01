using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.ProductRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductsByCategoryIdQueryHandler(IProductRepository productRepository)
     : IRequestHandler<GetProductsByCategoryIdQuery, List<GetProductsByCategoryQueryResult>>
    {
        public async Task<List<GetProductsByCategoryQueryResult>> Handle(
            GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var items = await productRepository.GetProductsByCategoryIdWithCategoryAsync(request.Id);
            return items.Adapt<List<GetProductsByCategoryQueryResult>>();
        }
    }
}
