using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactCommands;

public record RemoveContactCommand(Guid Id):IRequest;
