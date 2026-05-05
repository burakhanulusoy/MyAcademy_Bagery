using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BannerHandlers
{
    public class CreateBannerCommandHandler(IBannerRepository _bannerRepository
                                            ,IFileService _fileService
                                            ,IUnitOfWork _unitOfWork
                                            ,IValidator<CreateBannerCommand> _validator) : IRequestHandler<CreateBannerCommand>
    {
        public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }


            var uploadImage = await _fileService.UploadFile(request.File1);
            var uploadImage2 = await _fileService.UploadFile(request.File2);


            var mappedBanner = request.Adapt<Banner>();
            mappedBanner.ImageUrl = uploadImage;
            mappedBanner.BackgroundImageUrl= uploadImage2;

            await _bannerRepository.CreateAsync(mappedBanner);
            await _unitOfWork.SaveChangesAsync();



        }
    }
}
