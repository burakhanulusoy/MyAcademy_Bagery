using Bagery.WebUI.MediatorPattern.Commands.InstallmentCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // PAY_TR'de bu buton ödeme sayfasındaydı, herkes basabiliyordu
    public class InstallmentController(IMediator _mediator) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRates()
        {
            var count = await _mediator.Send(new UpdateInstallmentRatesCommand());
            return Json(new { success = true, message = $"{count} taksit oranı PayTR'den alınıp kaydedildi." });
        }
    }
}