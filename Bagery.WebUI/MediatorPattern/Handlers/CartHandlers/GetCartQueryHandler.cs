using Bagery.WebUI.MediatorPattern.Queries.CartQueries;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class GetCartQueryHandler(ICartService _cartService) : IRequestHandler<GetCartQuery, Cart>
    {
        public Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_cartService.GetCart()); // sadece okur, kaydetmez
        }
    }
}