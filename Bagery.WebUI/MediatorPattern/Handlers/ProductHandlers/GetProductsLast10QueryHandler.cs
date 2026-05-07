using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.ProductRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductsLast10QueryHandler(IProductRepository _productRepository) : IRequestHandler<GetProductLast10Query, List<GetProductsQueryResult>>
    {
        public async Task<List<GetProductsQueryResult>> Handle(GetProductLast10Query request, CancellationToken cancellationToken)
        {
            var item = await _productRepository.GetProductLast10WithCategory();
            return item.Adapt<List<GetProductsQueryResult>>();
        }
    }
}
