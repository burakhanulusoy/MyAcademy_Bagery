using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.RoleQueries
{
    public class GetRolesWithUserCountQuery:IRequest<List<GetRolesWithUserCountQueryResult>>
    {
    }
}
