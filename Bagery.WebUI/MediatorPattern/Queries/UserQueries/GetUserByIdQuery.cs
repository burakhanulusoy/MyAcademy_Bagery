using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.UserQueries;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdQueryResult>;