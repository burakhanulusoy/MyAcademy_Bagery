using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands;
using Bagery.WebUI.Repositories.ContactSocialMediaRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactSocialMediaHandlers
{
    public class UpdateContactSocialMediaCommandHandler(IContactSocialMediaRepository contactSocialMediaRepository,
                                                        IUnitOfWork unitOfWork,
                                                        IFileService fileService,
                                                        IValidator<UpdateContactSocialMediaCommand> _validator) : IRequestHandler<UpdateContactSocialMediaCommand>
    {
        public async Task Handle(UpdateContactSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<ContactSocialMedia>();

            if (request.File != null && request.File.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.IconUrl))
                {
                    await fileService.DeleteFileAsync(request.IconUrl);
                }
                mappedItem.IconUrl = await fileService.UploadFile(request.File);
            }

           

            contactSocialMediaRepository.Update(mappedItem);
            await unitOfWork.SaveChangesAsync();

        }
    }
}
