using Bagery.WebUI.Models.CartModels;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CartCommands;

// PAY_TR'deki CartRemove: satırı komple siler
public record RemoveCartItemCommand(Guid ProductId, Guid? VariantId) : IRequest<Cart>;