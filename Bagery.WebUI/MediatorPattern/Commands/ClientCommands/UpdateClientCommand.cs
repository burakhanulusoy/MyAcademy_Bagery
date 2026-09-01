using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ClientCommands;

public record UpdateClientCommand(Guid Id, string ClientImageUrl, string Name,IFormFile ClientImage):IRequest;