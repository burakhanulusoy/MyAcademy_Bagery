using Bagery.WebUI.MediatorPattern.Commands.ContactCommands;
using Bagery.WebUI.MediatorPattern.Queries.ContactQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class ContactController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var item = await _mediator.Send(new GetContactsQuery());
            return View(item);
        }

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            await _mediator.Send(new RemoveContactCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> UpdateContact(Guid id)
        {
            var item = await _mediator.Send(new GetContactByIdQuery(id));
            var updateItem = item.Adapt<UpdateContactCommand>();
            return View(updateItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }



    }
}
