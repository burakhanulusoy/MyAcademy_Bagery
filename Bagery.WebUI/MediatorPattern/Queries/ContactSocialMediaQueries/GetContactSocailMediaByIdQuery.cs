using Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ContactSocialMediaQueries;

public record GetContactSocailMediaByIdQuery(Guid Id):IRequest<GetContactSocialMediaByIdQueryResult>;
