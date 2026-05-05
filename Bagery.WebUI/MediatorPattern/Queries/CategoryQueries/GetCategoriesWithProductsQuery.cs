using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CategoryQueries
{
    public class GetCategoriesWithProductsQuery:IRequest<List<GetCategoriesWithProductsQueryResult>>
    {
    }
}
