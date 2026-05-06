using Bagery.WebUI.MediatorPattern.Queries.ProductVariantQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductVariantResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class GetProductVariantByIdQueryHandler : IRequestHandler<GetProductVariantByIdQuery, GetProductVariantByIdQueryResult>
    {
        public Task<GetProductVariantByIdQueryResult> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
