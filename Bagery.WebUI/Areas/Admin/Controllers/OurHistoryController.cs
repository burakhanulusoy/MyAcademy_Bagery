using Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;
using Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class OurHistoryController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var items = await mediator.Send(new GetOurHistoriesQuery());
            return View(items);
        }

        public IActionResult CreateOurHistory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOurHistory(CreateOurHistoryCommand command)
        {
            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> UpdateOurHistory(Guid id)
        {
            var item = await mediator.Send(new GetOurHistoryByIdQuery(id));
            var mappedItem = item.Adapt<UpdateOurHistoryCommand>();
            return View(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOurHistory(UpdateOurHistoryCommand command)
        {
            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteOurHistory(Guid id)
        {
            await mediator.Send(new RemoveOurHistoryCommand(id));
            return RedirectToAction(nameof(Index));

        }


    }
}
