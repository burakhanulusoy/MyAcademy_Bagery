using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;

public record CreateContactMessageCommand(string NameSurname, string Email, string PhoneNumber, string Subject, string Message): IRequest;
