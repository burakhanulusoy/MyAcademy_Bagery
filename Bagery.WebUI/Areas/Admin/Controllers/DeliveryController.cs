using Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;
using Bagery.WebUI.MediatorPattern.Queries.DeliveryQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DeliveryController(IMediator _mediator) : Controller
    {
        // /Admin/Delivery/Index: takip sayfası (veriyi JS çeker)
        public IActionResult Index()
        {
            return View();
        }

        // /Admin/Delivery/Data: pano verisi (her 15 sn)
        [HttpGet]
        public async Task<IActionResult> Data()
        {
            var board = await _mediator.Send(new GetDeliveryBoardQuery());
            return Json(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Advance(AdvanceDeliveryCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true });
        }

        // Admin için süre sınırı yok (UndoDeliveryCommandHandler rolüne bakıyor)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undo(UndoDeliveryCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true });
        }
    }
}