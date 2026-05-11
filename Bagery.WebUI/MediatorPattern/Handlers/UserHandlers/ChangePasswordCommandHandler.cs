using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class ChangePasswordCommandHandler(UserManager<AppUser> _userManager,
                                              IHttpContextAccessor _http,
                                              IValidator<ChangePasswordCommand> _validator,
                                              SignInManager<AppUser> _signInManager) : IRequestHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            var result = await _userManager.ChangePasswordAsync(user, request.NowPassword, request.NewPassword);

            if(!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }

            await _signInManager.SignOutAsync();








        }
    }
}
