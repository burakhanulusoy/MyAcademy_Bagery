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
    public class UpdateBannerCommandHandler(IBannerRepository _bannerRepository,
                                            IFileService _fileService,
                                            IUnitOfWork _unitOfWork,
                                            IValidator<UpdateBannerCommand> _validator) : IRequestHandler<UpdateBannerCommand>
    {
        public async Task Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedBanner = request.Adapt<Banner>();

            if (request.File1 != null && request.File1.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.ImageUrl);
                }
                mappedBanner.ImageUrl = await _fileService.UploadFile(request.File1);
            }

            if (request.File2 != null && request.File2.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.BackgroundImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.BackgroundImageUrl);
                }
                mappedBanner.BackgroundImageUrl = await _fileService.UploadFile(request.File2);
            }

            _bannerRepository.Update(mappedBanner);
            await _unitOfWork.SaveChangesAsync();




        }
    }
}
