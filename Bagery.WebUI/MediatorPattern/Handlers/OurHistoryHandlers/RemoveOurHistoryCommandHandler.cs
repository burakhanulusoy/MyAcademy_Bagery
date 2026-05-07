using Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OurHistoryHandlers
{
    public class RemoveOurHistoryCommandHandler(IOurHistoryRepository ourHistoryRepository,
                                                IUnitOfWork _unitOfWork,
                                                IFileService _fileService) : IRequestHandler<RemoveOurHistoryCommand>
    {
        public async Task Handle(RemoveOurHistoryCommand request, CancellationToken cancellationToken)
        {
            var item = await ourHistoryRepository.GetByIdAsync(request.id);

            await _fileService.DeleteFileAsync(item.ImageUrl);
            await _fileService.DeleteFileAsync(item.MainImageUrl);
            await _fileService.DeleteFileAsync(item.SignatureUrl);

            ourHistoryRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
