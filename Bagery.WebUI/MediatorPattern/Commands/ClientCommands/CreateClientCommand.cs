using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ClientCommands;

public record CreateClientCommand(string? Name,IFormFile? ClientImage):IRequest;
