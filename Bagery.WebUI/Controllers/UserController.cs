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


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordCommand(string.Empty, string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var origin = $"{Request.Scheme}://{Request.Host}";
            await _mediator.Send(command with { OriginUrl = origin });

            TempData["ForgotPasswordSuccess"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
            return RedirectToAction("ForgotPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordCommand(email, token, string.Empty, string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await _mediator.Send(command);

            TempData["ResetPasswordSuccess"] = "Şifreniz başarıyla güncellendi. Giriş yapabilirsiniz.";
            return RedirectToAction("Login");
        }






    }
}
