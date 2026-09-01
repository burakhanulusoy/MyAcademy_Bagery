using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ClientCommands;

public record RemoveClientCommand(Guid Id):IRequest;
