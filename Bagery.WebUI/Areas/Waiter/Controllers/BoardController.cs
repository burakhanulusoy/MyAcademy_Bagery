using Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;
using Bagery.WebUI.MediatorPattern.Queries.DeliveryQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    [Authorize(Roles = "Waiter")]
    public class BoardController(IMediator _mediator) : Controller
    {
        // /Waiter/Board/Index: pano ekranı (veriyi JS kendisi çeker)
        public IActionResult Index()
        {
            return View();
        }

        // /Waiter/Board/Data: pano verisi (JS her 15 sn'de bir çağırır)
        [HttpGet]
        public async Task<IActionResult> Data()
        {
            var board = await _mediator.Send(new GetDeliveryBoardQuery());
            return Json(board);
        }

        // "Yola çıktı" / "Teslim edildi"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Advance(AdvanceDeliveryCommand command)
        {
            await _mediator.Send(command); // hata olursa ExceptionFilter {success:false, message} döner
            return Json(new { success = true });
        }

        // "Geri al" (5 dk içinde; süre kontrolü handler'da)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undo(UndoDeliveryCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true });
        }
    }
}