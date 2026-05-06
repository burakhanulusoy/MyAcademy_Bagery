using Bagery.WebUI.MediatorPattern.Results.ProductVariantResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductVariantQueries
{
    public class GetProductVariantByIdQuery(Guid id):IRequest<GetProductVariantByIdQueryResult>
    {
        public Guid Id { get; set; } = id;

    }
}
