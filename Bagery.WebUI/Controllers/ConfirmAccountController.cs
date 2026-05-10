using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    public class ConfirmAccountController(IMediator _mediator) : Controller
    {
        [HttpGet]
        public IActionResult Index(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmAccountCommand command)
        {
                ViewBag.Email = command.Email;

                await _mediator.Send(command);
                return RedirectToAction("Login", "User");
          
        }
    }
}
