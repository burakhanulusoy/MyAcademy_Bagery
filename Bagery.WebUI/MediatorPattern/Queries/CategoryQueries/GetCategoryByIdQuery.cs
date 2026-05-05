using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery(Guid id) : IRequest<GetCategoryByIdQueryResult>
    {
        public Guid Id { get; set; } = id;


    }
}
