using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;

public record RemoveContactMessageCommand(Guid Id):IRequest;
