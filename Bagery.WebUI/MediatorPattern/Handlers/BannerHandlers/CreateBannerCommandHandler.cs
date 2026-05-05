using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using Bagery.WebUI.MediatorPattern.Comments.FileComments;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.UOW;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BannerHandlers
{
    public class CreateBannerCommandHandler(IBannerRepository _bannerRepository,IMediator _mediator,IUnitOfWork _unitOfWork) : IRequestHandler<CreateBannerCommand>
    {
        public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {

            var uploadImage = await _mediator.Send(new UploadFileCommand(request.File1));
            var uploadImage2 = await _mediator.Send(new UploadFileCommand(request.File2));


            var mappedBanner = request.Adapt<Banner>();
            mappedBanner.ImageUrl = uploadImage;
            mappedBanner.BackgroundImageUrl= uploadImage2;

            await _bannerRepository.CreateAsync(mappedBanner);
            await _unitOfWork.SaveChangesAsync();



        }
    }
}
