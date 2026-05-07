using Bagery.WebUI.MediatorPattern.Results.ContactResults;

namespace Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;

public record GetContactSocialMediaByIdQueryResult(Guid Id,
                                                   string IconUrl,
                                                   string SocialMediaName,
                                                   string SocialMediaUrl,
                                                   Guid ContactId
                                                   );