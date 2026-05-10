using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class UserController(IMediator _mediator) : Controller
    {
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index", "ConfirmAccount", new { email = command.Email });
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return RedirectToAction("Index", "Banner", new { area = "Admin" });
            }
            catch (IdentityException ex)
            {
                if (ex.Message == "EmailConfirmRequired")
                {
                    return RedirectToAction("Index", "ConfirmAccount", new { email = command.Email });
                }

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(command);


        }


    }
}
