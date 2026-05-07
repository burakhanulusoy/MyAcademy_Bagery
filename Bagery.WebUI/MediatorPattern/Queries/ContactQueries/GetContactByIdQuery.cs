using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactQueries;

public record GetContactByIdQuery(Guid Id):IRequest<GetContactByIdQueryResult>;
