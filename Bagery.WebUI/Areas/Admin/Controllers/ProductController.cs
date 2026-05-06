using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System.Threading.Tasks;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController(IMediator _mediator) : Controller
    {

        private async Task GetCategoriesAsync()
        {

            var categories = await _mediator.Send(new GetCategoriesQuery());

            ViewBag.Categories = (from cat in categories
                                  select new SelectListItem
                                  {
                                      Text = cat.CategoryName,
                                      Value = cat.Id.ToString()
                                  }).ToList();

        }



        public async Task<IActionResult> Index(int page = 1,int pageSize =12)
        {
            var items = await _mediator.Send(new GetProductsQuery());

            var pageItems = new PagedList<GetProductsQueryResult>(items.AsQueryable(),page,pageSize);

            return View(pageItems);
        }

        public async Task<IActionResult> CreateProduct()
        {
            await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            await GetCategoriesAsync();
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _mediator.Send(new RemoveProductCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            await GetCategoriesAsync();
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            var updateItem = item.Adapt<UpdateProductCommand>();
            return View(updateItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await GetCategoriesAsync();
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            return View(item);
        }







    }
}
