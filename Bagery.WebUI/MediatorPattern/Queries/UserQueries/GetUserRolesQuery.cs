using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.UserQueries;

public record GetUserRolesQuery(Guid Id):IRequest<List<GetUserRoleQueryResult>>;