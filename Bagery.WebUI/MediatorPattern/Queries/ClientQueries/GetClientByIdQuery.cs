using Bagery.WebUI.MediatorPattern.Results.ClientResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ClientQueries;

public record GetClientByIdQuery(Guid Id):IRequest<GetClientByIdQueryResult>;
