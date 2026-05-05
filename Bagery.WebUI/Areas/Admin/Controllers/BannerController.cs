using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using Bagery.WebUI.MediatorPattern.Queries.BannerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var banners = await _mediator.Send(new GetBannersQuery());
            return View(banners);
        }

        public IActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }


    }
}
