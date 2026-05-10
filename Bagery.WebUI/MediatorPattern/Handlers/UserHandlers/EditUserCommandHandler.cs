using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.Services;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class EditUserCommandHandler(IFileService _fileService,
                                        UserManager<AppUser> _userManager,
                                        IHttpContextAccessor _http,
                                        IValidator<EditUserCommand> validator) : IRequestHandler<EditUserCommand>
    {
        public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            
            if(!passwordCheck)
            {
                throw new IdentityException("Lütfen şifrenizi kontrol ediniz");
            }

            var oldImageUrl = user.ImageUrl;

            request.Adapt(user);


            if (request.file is not null)
            {
               user.ImageUrl = await _fileService.UploadFile(request.file);
            }
            else
            {
                user.ImageUrl = oldImageUrl;
            }

            var result = await _userManager.UpdateAsync(user);
           
            if(!result.Succeeded)
            {
                if(request.file is not null)
                {
                    await _fileService.DeleteFileAsync(user.ImageUrl);
                }


                throw new IdentityException(result.Errors);
            }

            if (request.file is not null && !string.IsNullOrEmpty(oldImageUrl))
            {
                await _fileService.DeleteFileAsync(oldImageUrl);
            }



        }
    }
}
