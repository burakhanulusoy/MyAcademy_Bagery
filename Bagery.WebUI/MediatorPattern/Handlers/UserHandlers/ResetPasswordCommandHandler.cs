using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class ResetPasswordCommandHandler(UserManager<AppUser> _userManager,IValidator<ResetPasswordCommand> _validator) : IRequestHandler<ResetPasswordCommand, bool>
    {
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new IdentityException("Kullanıcı bulunamadı.");

            // 2. KRİTİK: URL'den gelen güvenli token'ı Identity'nin anlayacağı hale geri çevir
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
            var actualToken = Encoding.UTF8.GetString(decodedTokenBytes);

            // 3. Şifreyi sıfırla
            var result = await _userManager.ResetPasswordAsync(user, actualToken, request.NewPassword);

            if (!result.Succeeded)
            {
                // Eğer hala geçersiz token diyorsa Identity hatalarını fırlat
                throw new IdentityException(result.Errors.First().Description);
            }

            return true;
        }
    }
}
