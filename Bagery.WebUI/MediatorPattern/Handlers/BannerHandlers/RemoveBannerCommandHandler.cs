using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BannerHandlers;

public class RemoveBannerCommandHandler(IFileService _fileService
                                        ,IBannerRepository _bannerRepository
                                        ,IUnitOfWork _unitOfWork) : IRequestHandler<RemoveBannerCommand>
{
    public async Task Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
    {

        var banner = await _bannerRepository.GetByIdAsync(request.Id);

        await _fileService.DeleteFileAsync(banner.ImageUrl);
        await _fileService.DeleteFileAsync(banner.BackgroundImageUrl);

        _bannerRepository.Delete(banner);
        await _unitOfWork.SaveChangesAsync();




    }
}
