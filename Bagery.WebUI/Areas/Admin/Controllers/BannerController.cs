using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using Bagery.WebUI.MediatorPattern.Queries.BannerQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> DeleteBanner(Guid id)
        {
            await _mediator.Send(new RemoveBannerCommand(id));
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> UpdateBanner(Guid id)
        {

            var item = await _mediator.Send(new GetBannerByIdQuery(id));
            var updateItem = item.Adapt<UpdateBannerCommand>();
            return View(updateItem);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }



    }
}
