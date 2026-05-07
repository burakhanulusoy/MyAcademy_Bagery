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
    public class UpdateOurHistoryCommandHandler(IOurHistoryRepository ourHistoryRepository,
                                                IValidator<UpdateOurHistoryCommand> validator,
                                                IUnitOfWork _unitOfWork,
                                                IFileService _fileService) : IRequestHandler<UpdateOurHistoryCommand>
    {
        public async Task Handle(UpdateOurHistoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<OurHistory>();

            if (request.File != null && request.File.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.ImageUrl);
                }
                mappedItem.ImageUrl = await _fileService.UploadFile(request.File);
            }

            if (request.MainFile != null && request.MainFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.MainImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.MainImageUrl);
                }
                mappedItem.MainImageUrl = await _fileService.UploadFile(request.MainFile);
            }

            if (request.SignatureFile != null && request.SignatureFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.SignatureUrl))
                {
                    await _fileService.DeleteFileAsync(request.SignatureUrl);
                }
                mappedItem.SignatureUrl = await _fileService.UploadFile(request.SignatureFile);
            }

            ourHistoryRepository.Update(mappedItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
