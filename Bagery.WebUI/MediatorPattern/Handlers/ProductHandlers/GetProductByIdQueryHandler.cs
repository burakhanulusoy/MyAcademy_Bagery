using AspNetCoreGeneratedDocument;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Bagery.WebUI.Repositories.ProductRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var item = await productRepository.GetProductByIdWithProductVariants(request.Id);
            return item.Adapt<GetProductByIdQueryResult>();


        }
    }
}
