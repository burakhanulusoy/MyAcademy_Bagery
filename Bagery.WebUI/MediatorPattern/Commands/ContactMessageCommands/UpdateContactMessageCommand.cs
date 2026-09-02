using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;

public record UpdateContactMessageCommand(Guid Id,string NameSurname, string Email, string PhoneNumber, string Subject, string Message):IRequest;