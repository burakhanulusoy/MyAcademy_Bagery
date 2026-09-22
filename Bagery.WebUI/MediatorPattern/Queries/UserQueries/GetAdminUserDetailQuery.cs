using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.UserQueries;

public record GetAdminUserDetailQuery(Guid Id) : IRequest<GetAdminUserDetailQueryResult?>;