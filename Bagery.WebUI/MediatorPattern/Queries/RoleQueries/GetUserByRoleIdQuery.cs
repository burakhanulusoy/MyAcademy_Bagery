using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.RoleQueries;

public record GetUserByRoleIdQuery(Guid Id):IRequest<List<GetUserByRolIdQueryResult>>;
