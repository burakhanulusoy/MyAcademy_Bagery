using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents._AdminLoyoutComponents.ForProductViewComponents
{
    public class GetLast10ProductViewComponents(IMediator mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var items = await mediator.Send(new GetProductLast10Query());
            return View(items);
        }

    }
}
