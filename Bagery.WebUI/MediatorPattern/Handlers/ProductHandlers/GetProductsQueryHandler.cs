using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.ProductRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, List<GetProductsQueryResult>>
    {
        public async Task<List<GetProductsQueryResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var item = await productRepository.GetProductsWithCategoryAsync();
            return item.Adapt<List<GetProductsQueryResult>>();

        }
    }
}
