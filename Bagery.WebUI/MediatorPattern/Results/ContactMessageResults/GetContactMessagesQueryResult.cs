using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.ContactMessageResults;

public record GetContactMessagesQueryResult(Guid Id,
                                           string NameSurname,
                                           string Email,
                                           string PhoneNumber,
                                           string Subject,
                                           string Message,
                                           ContactMessageStatus MessageStatus,
                                           DateTime CreatedAt
                                           );
