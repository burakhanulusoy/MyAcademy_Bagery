using Bagery.WebUI.MediatorPattern.Results.ContactMessageResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactMessageQueries;

public record GetContactMessagesQuery:IRequest<List<GetContactMessagesQueryResult>>;