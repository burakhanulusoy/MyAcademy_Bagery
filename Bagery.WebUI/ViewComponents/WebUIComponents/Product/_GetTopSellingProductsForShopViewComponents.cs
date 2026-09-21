using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Product
{
    // Mağaza sayfasının sol sidebar'ındaki küçük liste
    public class _GetTopSellingProductsForShopViewComponents(IMediator _mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _mediator.Send(new GetTopSellingProductsQuery(4)); // aynı handler
            return View(products);
        }
    }
}