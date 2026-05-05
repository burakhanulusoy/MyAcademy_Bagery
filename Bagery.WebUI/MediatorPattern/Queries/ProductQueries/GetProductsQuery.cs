using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductQueries
{
    public class GetProductsQuery:IRequest<List<GetProductsQueryResult>>
    {
    }
}
