using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.Services.EmailServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class LoginUserCommandHandler(UserManager<AppUser> _userManager
        , SignInManager<AppUser> _signInManager,
        IEmailService _emailService) : IRequestHandler<LoginUserCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new IdentityException("Böyle bir kullanıcı bulunamadı.");

            if (user.IsDeleted)
            {
                throw new IdentityException("Bu kullanıcın hesabı kapatıldı.");
            }


            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);



            if (result.IsLockedOut)
                throw new IdentityException("Hesabınız kilitlendi.");

            if (!result.Succeeded)
                throw new IdentityException("Email veya şifreniz yanlış.");

            if (!user.EmailConfirmed)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await _emailService.SendEmailFor2FactorAuthentication(user.Email!, code, user.FullName!);
                throw new IdentityException("EmailConfirmRequired");
            }

            // 3. Her şey tamamsa şimdi gerçekten giriş yap (Cookie oluştur)
            await _signInManager.SignInAsync(user, request.RememberMe);

            var roles = await _userManager.GetRolesAsync(user);

            return roles;

        }
    }
}
