using Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;
using Bagery.WebUI.Repositories.PromotionRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PromotionHandlers
{
    public class RemovePromotionCommandHandler(IPromotionRepository promotionRepository
                                               , IFileService fileService,
                                                IUnitOfWork unitOfWork) : IRequestHandler<RemovePromotionCommand>
    {
        public async Task Handle(RemovePromotionCommand request, CancellationToken cancellationToken)
        {
            var banner = await promotionRepository.GetByIdAsync(request.Id);

            await fileService.DeleteFileAsync(banner.ImageUrl);

            promotionRepository.Delete(banner);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
