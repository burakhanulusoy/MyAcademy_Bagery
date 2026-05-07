using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactQueries;

public record GetContactsQuery : IRequest<List<GetContactsWithSocialMediaQueryResult>>;

