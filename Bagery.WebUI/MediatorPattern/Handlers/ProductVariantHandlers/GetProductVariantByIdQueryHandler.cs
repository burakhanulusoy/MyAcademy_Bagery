using Bagery.WebUI.MediatorPattern.Queries.ProductVariantQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductVariantResults;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class GetProductVariantByIdQueryHandler(IProductVariantRepository _productVariantRepository) : IRequestHandler<GetProductVariantByIdQuery, GetProductVariantByIdQueryResult>
    {
        public async Task<GetProductVariantByIdQueryResult> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
             
              var item = await _productVariantRepository.GetByIdAsync(request.Id);
              return item.Adapt<GetProductVariantByIdQueryResult>();



        }
    }
}
