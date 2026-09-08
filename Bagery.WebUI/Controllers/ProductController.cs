using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Queries.ShopPageQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class ProductController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return View(result);
        }


        public async Task<IActionResult> Shop(Guid? categoryId, string? search, decimal? min, decimal? max, int page = 1)
        {
            var result = await _mediator.Send(new GetShopPageQuery(categoryId, search, min, max, page));
            return View(result);
        }






    }
}
