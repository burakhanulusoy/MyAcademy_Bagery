using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Product
{
    // Ürün detay sayfasının altındaki 4'lü kart bölümü
    public class _GetTopSellingProductsForDetailViewComponents(IMediator _mediator) : ViewComponent
    {
        // productId: şu an açık olan ürün; listede kendisi tekrar görünmesin
        public async Task<IViewComponentResult> InvokeAsync(Guid productId)
        {
            var products = await _mediator.Send(new GetTopSellingProductsQuery(4, productId));
            return View(products);
        }
    }
}