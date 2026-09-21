using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CartCommands;

// Ödeme başarılı olunca Success sayfasında çağrılacak. Geriye bir şey dönmüyor.
public record ClearCartCommand : IRequest;