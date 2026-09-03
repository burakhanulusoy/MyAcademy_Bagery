using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactQueries;

public record GetContactLastQuery:IRequest<GetContactLastQueryResult>;
