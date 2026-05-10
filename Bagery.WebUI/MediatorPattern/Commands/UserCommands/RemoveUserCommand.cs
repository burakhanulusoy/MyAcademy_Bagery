using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands;

public record RemoveUserCommand(Guid Id):IRequest;   
