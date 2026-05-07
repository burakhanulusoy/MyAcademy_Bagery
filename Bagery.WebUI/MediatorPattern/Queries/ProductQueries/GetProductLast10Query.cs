using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductQueries
{
    public class GetProductLast10Query:IRequest<List<GetProductsQueryResult>>
    {


    }
}
