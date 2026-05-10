using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.Services.EmailServices;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class RegisterUserCommandHandler(UserManager<AppUser> _userManager,IValidator<RegisterUserCommand> validator
                                            ,IEmailService _emailService) : IRequestHandler<RegisterUserCommand>
    {
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }



            if(request.Password != request.ConfirmPassword)
            {
                throw new IdentityException("Şifreler birbiriyle uyuşmuyor. Lütfen kontrol ediniz.");
            }

            var user = request.Adapt<AppUser>();

            var result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }


            var sixDigitCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            await _emailService.SendEmailFor2FactorAuthentication(user.Email,sixDigitCode,user.FullName);
                


        }
    }
}
