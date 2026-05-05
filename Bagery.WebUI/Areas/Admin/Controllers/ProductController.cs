using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetProductsQuery());
            return View(items);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
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
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            var updateItem = item.Adapt<UpdateProductCommand>();
            return View(updateItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
