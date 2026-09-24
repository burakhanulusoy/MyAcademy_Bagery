using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GoogleCallbackCommandHandler(
        UserManager<AppUser> _userManager,
        SignInManager<AppUser> _signInManager)
        : IRequestHandler<GoogleCallbackCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(
            GoogleCallbackCommand request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.RemoteError))
                throw new IdentityException("Google ile giriş iptal edildi veya bir sorun oluştu.");

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info is null)
                throw new IdentityException("Google bilgileri alınamadı. Lütfen tekrar deneyin.");

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(email))
                throw new IdentityException("Google hesabınızdan e-posta bilgisi alınamadı.");

            // 1) Daha önce bu Google hesabı bağlanmış mı?
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            // 2) Bağlanmamışsa, aynı e-postayla kayıtlı biri var mı?
            user ??= await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                if (user.IsDeleted)
                    throw new IdentityException("Bu kullanıcının hesabı kapatıldı.");

                // E-postayla kayıtlıydı ama Google bağlı değilse şimdi bağla
                var logins = await _userManager.GetLoginsAsync(user);

                if (!logins.Any(x => x.LoginProvider == info.LoginProvider
                                  && x.ProviderKey == info.ProviderKey))
                {
                    var addLogin = await _userManager.AddLoginAsync(user, info);

                    if (!addLogin.Succeeded)
                        throw new IdentityException(addLogin.Errors);
                }

                // Google e-postayı doğruladığı için onay bekleyen hesabı açıyoruz
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }

                // YENİ: rolü olmayan eski hesaplar panelsiz kalmasın
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (currentRoles.Count == 0)
                {
                    var assignRole = await _userManager.AddToRoleAsync(user, "User");

                    if (!assignRole.Succeeded)
                        throw new IdentityException(assignRole.Errors);
                }
            }
            else
            {
                // 3) Hiç yoksa yeni kullanıcı oluştur
                user = new AppUser
                {
                    Email = email,
                    UserName = await GenerateUserNameAsync(email),
                    FullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? email,
                    ImageUrl = info.Principal.FindFirstValue("picture"),
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    throw new IdentityException(createResult.Errors);

                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                    throw new IdentityException(roleResult.Errors);

                var loginResult = await _userManager.AddLoginAsync(user, info);

                if (!loginResult.Succeeded)
                    throw new IdentityException(loginResult.Errors);
            }

            // Geçici external cookie'yi temizle
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, isPersistent: true);

            return await _userManager.GetRolesAsync(user);
        }

        private async Task<string> GenerateUserNameAsync(string email)
        {
            var baseName = new string(email.Split('@')[0]
                .Where(c => char.IsLetterOrDigit(c) || c == '_')
                .ToArray());

            if (string.IsNullOrWhiteSpace(baseName))
                baseName = "kullanici";

            var candidate = baseName;
            var counter = 1;

            while (await _userManager.FindByNameAsync(candidate) is not null)
            {
                candidate = $"{baseName}{counter}";
                counter++;
            }

            return candidate;
        }
    }
}