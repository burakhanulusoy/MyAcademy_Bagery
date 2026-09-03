using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.MediatorPattern.Queries.ContactQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class ContactController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetContactLastQuery());
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactMessage(CreateContactMessageCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, email = command.Email });
        }

        [HttpPost]
        public async Task<IActionResult> VerifyContactMessage(VerifyContactMessageCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true });
        }
    }
}
