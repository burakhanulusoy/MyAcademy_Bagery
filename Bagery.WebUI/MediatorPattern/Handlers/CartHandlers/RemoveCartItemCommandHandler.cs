using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class RemoveCartItemCommandHandler(ICartService _cartService) : IRequestHandler<RemoveCartItemCommand, Cart>
    {
        public Task<Cart> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartService.GetCart();
            cart.RemoveItem(request.ProductId, request.VariantId);
            _cartService.SaveCart(cart);
            return Task.FromResult(cart);
        }
    }
}