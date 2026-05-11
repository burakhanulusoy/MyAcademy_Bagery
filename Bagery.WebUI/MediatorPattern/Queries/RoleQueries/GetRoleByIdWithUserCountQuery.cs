using Bagery.WebUI.MediatorPattern.Results.RoleResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.RoleQueries;

public record GetRoleByIdWithUserCountQuery(Guid Id):IRequest<GetRoleByIdWithUserCountQueryResult>;