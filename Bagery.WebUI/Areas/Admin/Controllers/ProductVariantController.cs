using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Queries.ProductVariantQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductVariantController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> GetVariantsByPorductId(Guid id) //prodcut id
        {
            ViewBag.PorductId = id;
            var item = await _mediator.Send(new GetProductByIdQuery(id));
            return View(item);
        }

        public IActionResult CreateVariantByProductId(Guid id) //PRODCUT İD 
        {
     
            ViewBag.ProductId = id;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateVariantByProductId(CreateProductVariantCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("GetVariantsByPorductId", new { id = command.ProductId });


        }

        public async Task<IActionResult> UpdateVariant(Guid id)//prıductvariant id
        {
            var item = await _mediator.Send(new GetProductVariantByIdQuery(id));
            var mappedItem = item.Adapt<UpdateProductVariantCommand>();
            return View(mappedItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateVariant(UpdateProductVariantCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("GetVariantsByPorductId", new { id = command.ProductId });
        }

        public async Task<IActionResult> MakeActive(Guid id, Guid productId)
        {
            await _mediator.Send(new MakeActiveProductVariantCommand(id));
            return RedirectToAction("GetVariantsByPorductId", new { id = productId });
        }

        public async Task<IActionResult> MakePassive(Guid id, Guid productId)
        {
            await _mediator.Send(new MakePassiveProductVariantCommand(id));
            return RedirectToAction("GetVariantsByPorductId", new { id = productId });
        }


    }
}
