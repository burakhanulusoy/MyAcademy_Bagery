using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;
using Bagery.WebUI.Repositories.PromotionRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PromotionHandlers
{
    public class UpdatePromotionCommandHandler(IPromotionRepository promotionRepository
                                               , IFileService fileService,
                                                IUnitOfWork unitOfWork,
                                                IValidator<UpdatePromotionCommand> validator) : IRequestHandler<UpdatePromotionCommand>
    {
        public async Task Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedBanner = request.Adapt<Promotion>();

            if (request.file != null && request.file.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl))
                {
                    await fileService.DeleteFileAsync(request.ImageUrl);
                }
                mappedBanner.ImageUrl = await fileService.UploadFile(request.file);
            }


            promotionRepository.Update(mappedBanner);
            await unitOfWork.SaveChangesAsync();


        }
    }
}
