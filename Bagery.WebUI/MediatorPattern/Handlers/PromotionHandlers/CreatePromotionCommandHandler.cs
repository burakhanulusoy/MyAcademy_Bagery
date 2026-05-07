using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;
using Bagery.WebUI.Repositories.PromotionRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;
using NuGet.Protocol.Plugins;

namespace Bagery.WebUI.MediatorPattern.Handlers.PromotionHandlers
{
    public class CreatePromotionCommandHandler(IPromotionRepository promotionRepository
                                               , IFileService fileService,
                                                IUnitOfWork unitOfWork,
                                                IValidator<CreatePromotionCommand> validator) : IRequestHandler<CreatePromotionCommand>
    {
        public async Task Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }


            var uploadImage = await fileService.UploadFile(request.file);


            var mappedBanner = request.Adapt<Promotion>();
            mappedBanner.ImageUrl = uploadImage;

            await promotionRepository.CreateAsync(mappedBanner);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
