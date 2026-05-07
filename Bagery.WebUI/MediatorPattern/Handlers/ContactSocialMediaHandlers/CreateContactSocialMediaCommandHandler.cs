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
    public class CreateContactSocialMediaCommandHandler(IContactSocialMediaRepository contactSocialMediaRepository,
                                                        IUnitOfWork unitOfWork,
                                                        IFileService fileService,
                                                        IValidator<CreateContactSocialMediaCommand> validator) : IRequestHandler<CreateContactSocialMediaCommand>
    {
        public async Task Handle(CreateContactSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<ContactSocialMedia>();
            mappedItem.IconUrl = await fileService.UploadFile(request.File);
            

            await contactSocialMediaRepository.CreateAsync(mappedItem);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
