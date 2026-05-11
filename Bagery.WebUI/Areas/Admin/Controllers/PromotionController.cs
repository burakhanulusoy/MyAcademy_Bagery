using Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;
using Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PromotionController (IMediator _mediator): Controller
    {

        public async Task<IActionResult> Index()
        {

            var items = await _mediator.Send(new GetPromotionsQuery());
            return View(items);
        }

        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            await _mediator.Send(new RemovePromotionCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreatePromotion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePromotion(CreatePromotionCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdatePromotion(Guid Id)
        {
            var item= await _mediator.Send(new GetPromotionByIdQuery(Id));
            var mappedItem = item.Adapt<UpdatePromotionCommand>();
            return View(mappedItem);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePromotion(UpdatePromotionCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }



    }
}
