using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class ClearCartCommandHandler(ICartService _cartService) : IRequestHandler<ClearCartCommand>
    {
        public Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            _cartService.ClearCart();
            return Task.CompletedTask; // geriye değer dönmeyen handler'da "bitti" demek
        }
    }
}