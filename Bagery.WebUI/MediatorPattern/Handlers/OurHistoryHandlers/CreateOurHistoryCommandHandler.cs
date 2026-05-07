using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OurHistoryHandlers
{
    public class CreateOurHistoryCommandHandler(IOurHistoryRepository ourHistoryRepository,
                                                IValidator<CreateOurHistoryCommand> validator,
                                                IUnitOfWork _unitOfWork,
                                                IFileService _fileService) : IRequestHandler<CreateOurHistoryCommand>
    {
        public async Task Handle(CreateOurHistoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<OurHistory>();
            mappedItem.MainImageUrl = await _fileService.UploadFile(request.MainFile);
            mappedItem.ImageUrl = await _fileService.UploadFile(request.File);
            mappedItem.SignatureUrl = await _fileService.UploadFile(request.SignatureFile);


            await ourHistoryRepository.CreateAsync(mappedItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
