using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using Bagery.WebUI.MediatorPattern.Commands.ClientCommands;
using Bagery.WebUI.Repositories.ClientRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ClientHandlers
{
    public class UpdateClientCommandHandler(IClientRepository _clientRepository,
                                            IFileService fileService,
                                            IUnitOfWork _unitOfWork,
                                            IValidator<UpdateClientCommand> _validator) : IRequestHandler<UpdateClientCommand>
    {
        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }
            var mappedItem = request.Adapt<Client>();

            if (request.ClientImage!= null && request.ClientImage.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ClientImageUrl))
                {
                    await fileService.DeleteFileAsync(request.ClientImageUrl);
                }
                mappedItem.ClientImageUrl = await fileService.UploadFile(request.ClientImage);
            }

            _clientRepository.Update(mappedItem);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
