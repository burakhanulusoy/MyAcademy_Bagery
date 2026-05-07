using Bagery.WebUI.MediatorPattern.Results.ContactResults;

namespace Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;

public record GetContacSocialMediasQueryResult(Guid Id,
                                               string IconUrl,
                                               string SocialMediaName,
                                               Guid ContactId,
                                               GetContactByContactSocialMediaQueryResult Contact);




