using Bagery.WebUI.MediatorPattern.Commands.ClientCommands;
using Bagery.WebUI.Repositories.ClientRepositories;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ClientHandlers
{
    public class RemoveClientCommandHandler(IClientRepository _clientRepository,
                                            IFileService fileService,
                                            IUnitOfWork _unitOfWork) : IRequestHandler<RemoveClientCommand>
    {
        public async Task Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            var item = await _clientRepository.GetByIdAsync(request.Id);

            await fileService.DeleteFileAsync(item.ClientImageUrl);

            _clientRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
