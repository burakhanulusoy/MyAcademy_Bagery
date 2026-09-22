using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Writer")]
    public class ProfileController(IMediator _mediator) : Controller
    {
        // /Writer/Profile/UpdateUser
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _mediator.Send(new GetUserByManagerQuery());
            return View(user.Adapt<EditUserCommand>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(EditUserCommand command)
        {
            await _mediator.Send(command); // hata olursa ExceptionFilter formu mesajlarla geri açar
            TempData["PanelSuccess"] = "Profilin güncellendi.";
            return RedirectToAction(nameof(UpdateUser));
        }

        // /Writer/Profile/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command); // hata olursa ExceptionFilter formu mesajlarla geri açar

            // Handler başarılı değişiklikten sonra oturumu kapatıyor: yeni şifreyle tekrar giriş
            return RedirectToAction("Login", "User", new { area = string.Empty });
        }
    }
}