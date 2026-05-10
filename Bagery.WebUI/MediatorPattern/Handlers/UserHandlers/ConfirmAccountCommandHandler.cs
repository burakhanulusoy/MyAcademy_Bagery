using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class ConfirmAccountCommandHandler(UserManager<AppUser> _userManager) : IRequestHandler<ConfirmAccountCommand>
    {
        public async Task Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user == null)
            {
                throw new IdentityException("Kullanıcı bulunamadı.");
            }

            //sorgulama 
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Code!);

            if (!isValid)
            {
                throw new IdentityException("Girdiğiniz doğrulama kodu hatalı veya süresi dolmuş.");
            }

   
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);



        }
    }
}
