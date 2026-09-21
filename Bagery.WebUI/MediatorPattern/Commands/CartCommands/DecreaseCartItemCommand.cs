using Bagery.WebUI.Models.CartModels;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CartCommands;

// PAY_TR'deki CartDecrease: adedi 1 azaltır
public record DecreaseCartItemCommand(Guid ProductId, Guid? VariantId) : IRequest<Cart>;