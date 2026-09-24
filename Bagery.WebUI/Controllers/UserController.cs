using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login(LoginUserCommand command, string? returnUrl = null)
        {
            try
            {
                var userRoles = await _mediator.Send(command);

                // [Authorize] bir sayfadan gelindiyse (ör. /Payment/Checkout) oraya geri dön
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToPanel(userRoles);
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
            return RedirectToAction("Index", "Default", new { area = string.Empty });
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

        [HttpPost]
        public async Task<IActionResult> GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleCallback", "User")!;

            var properties = await _mediator.Send(new GoogleLoginCommand(redirectUrl));

            return Challenge(properties, "Google");
        }

        public async Task<IActionResult> GoogleCallback(string? remoteError = null)
        {
            try
            {
                var userRoles = await _mediator.Send(new GoogleCallbackCommand(remoteError));

                // DEĞİŞTİ: normal girişle aynı yönlendirme kullanılıyor
                return RedirectToPanel(userRoles);
            }
            catch (IdentityException ex)
            {
                TempData["LoginError"] = ex.Message;
                return RedirectToAction("Login");
            }
        }

        // YENİ: rol sıralaması tek yerde; hem normal giriş hem Google girişi aynı hedeflere gider
        private IActionResult RedirectToPanel(IList<string> roles)
        {
            if (roles.Contains("Admin"))
                return RedirectToAction("Dashboard", "Static", new { area = "Admin" });

            if (roles.Contains("Waiter"))
                return RedirectToAction("Index", "Board", new { area = "Waiter" });

            if (roles.Contains("Writer"))
                return RedirectToAction("Dashboard", "Static", new { area = "Writer" });

            if (roles.Contains("User"))
                return RedirectToAction("Index", "Static", new { area = "User" });

            // Rolü olmayan hesap: şablon sayfası yerine sitenin ana sayfası
            return RedirectToAction("Index", "Default", new { area = string.Empty });
        }
    }
}