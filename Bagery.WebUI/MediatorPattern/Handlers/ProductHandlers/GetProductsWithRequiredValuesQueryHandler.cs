using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.ProductRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductsWithRequiredValuesQueryHandler(IProductRepository _productRepository) : IRequestHandler<GetProductsWithRequiredValuesQuery, List<GetProductsWithRequiredValuesQueryResult>>
    {
        public async Task<List<GetProductsWithRequiredValuesQueryResult>> Handle(GetProductsWithRequiredValuesQuery request, CancellationToken cancellationToken)
        {
            var values = await _productRepository.GetAllAsync();
            return values.Adapt<List<GetProductsWithRequiredValuesQueryResult>>();
        }
    }
}
