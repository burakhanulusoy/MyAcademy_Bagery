using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class AccountController(IMediator _mediator) : Controller
    {
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            ViewBag.Email = email;

            var originUrl = $"{Request.Scheme}://{Request.Host}";

            await _mediator.Send(new ForgotPasswordCommand(email, originUrl));

            TempData["SuccessMessage"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";

            return RedirectToAction("ForgotPassword");
        }


        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordCommand(email, token, "", "");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi. Giriş yapabilirsiniz.";
                return RedirectToAction("Login", "User");
            }

            return View(command);
        }



    }
}
