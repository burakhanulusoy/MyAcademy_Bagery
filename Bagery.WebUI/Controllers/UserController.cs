using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
                var userRoles = await _mediator.Send(command);

                if (userRoles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Banner", new { area = "Admin" });
                }

                if (userRoles.Contains("Writer"))
                {
                    return RedirectToAction("Dashboard", "Static", new { area = "Writer" });
                }

                if (userRoles.Contains("User"))
                {
                    return RedirectToAction("Index", "Static", new { area = "User" });
                }

                return RedirectToAction("Index", "Home");
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


        public IActionResult BackToMainSite()
        {
            return RedirectToAction("Index", "Default", new { Area = string.Empty });
        }

        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutUserCommand());

            return RedirectToAction("Index", "Default", new { Area = string.Empty });
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("User/PageNotFound")]
        public IActionResult PageNotFound(int code)
        {
            // code parametresi buraya "404" olarak gelir. 
            return View();
        }

    }
}
