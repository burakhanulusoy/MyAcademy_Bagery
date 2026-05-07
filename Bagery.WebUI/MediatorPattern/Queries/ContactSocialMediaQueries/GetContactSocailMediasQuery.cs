using Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactSocialMediaQueries;

public record GetContactSocailMediasQuery:IRequest<List<GetContacSocialMediasQueryResult>>;
