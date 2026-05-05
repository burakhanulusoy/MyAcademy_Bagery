using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductQueries
{
    public class GetProductByIdQuery(Guid id):IRequest<GetProductByIdQueryResult>
    {
        public Guid Id { get; set; } = id;
    }
}
