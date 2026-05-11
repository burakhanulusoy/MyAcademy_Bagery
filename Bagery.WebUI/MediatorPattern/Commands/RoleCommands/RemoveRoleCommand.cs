using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.RoleCommands;

public record RemoveRoleCommand(Guid Id):IRequest;
