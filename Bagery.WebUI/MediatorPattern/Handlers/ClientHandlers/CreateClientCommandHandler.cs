using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ClientCommands;
using Bagery.WebUI.Repositories.ClientRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ClientHandlers
{
    public class CreateClientCommandHandler(IClientRepository _clientRepository,
                                            IFileService fileService,
                                            IUnitOfWork _unitOfWork,
                                            IValidator<CreateClientCommand> _validator) : IRequestHandler<CreateClientCommand>
    {
        public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<Client>();

            mappedItem.ClientImageUrl = await fileService.UploadFile(request.ClientImage);

            await _clientRepository.CreateAsync(mappedItem);
            await _unitOfWork.SaveChangesAsync();




        }
    }
}
