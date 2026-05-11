using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.Services.EmailServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class ForgotPasswordCommandHandler(UserManager<AppUser> _userManager
                                              ,IEmailService _emailService,
                                               IValidator<ForgotPasswordCommand> validator
                                               ) : IRequestHandler<ForgotPasswordCommand, bool>
    {
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user is null)
            {
                throw new IdentityException("Bu maile sahip kullanıcı bulunamadı.");
            }

            if (user.IsDeleted)
            {
                throw new IdentityException("Bu maile sahip kullanıcı hesabı kapatıldı.");
            }



            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var resetLink = $"{request.OriginUrl}/Account/ResetPassword?email={request.Email}&token={encodedToken}";

            await _emailService.SendPasswordResetLinkAsync(request.Email, resetLink, user.FullName);

            return true;


        }
    }
}
