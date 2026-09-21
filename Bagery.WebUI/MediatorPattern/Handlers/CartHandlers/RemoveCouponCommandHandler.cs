using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class RemoveCouponCommandHandler(ICartService _cartService) : IRequestHandler<RemoveCouponCommand, Cart>
    {
        public Task<Cart> Handle(RemoveCouponCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartService.GetCart();
            cart.RemoveCoupon();
            _cartService.SaveCart(cart);
            return Task.FromResult(cart);
        }
    }
}