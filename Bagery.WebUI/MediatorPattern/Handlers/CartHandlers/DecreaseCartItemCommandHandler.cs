using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class DecreaseCartItemCommandHandler(ICartService _cartService) : IRequestHandler<DecreaseCartItemCommand, Cart>
    {
        public Task<Cart> Handle(DecreaseCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartService.GetCart();                       // oku
            cart.DecreaseItem(request.ProductId, request.VariantId); // değiştir
            _cartService.SaveCart(cart);                             // kaydet
            return Task.FromResult(cart);
        }
    }
}