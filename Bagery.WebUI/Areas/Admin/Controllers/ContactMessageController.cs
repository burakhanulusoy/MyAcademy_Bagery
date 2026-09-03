using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.MediatorPattern.Queries.ContactMessageQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactMessageController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetContactMessagesQuery());
            return View(items);
        }

        public async Task<IActionResult> DetailContactMessage(Guid id)
        {
            var item = await _mediator.Send(new GetContactByIdMessagesQuery(id));
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactMessageStatus(UpdateContactMessageCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteContactMessage(Guid id)
        {
            await _mediator.Send(new RemoveContactMessageCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}