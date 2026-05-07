using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler(IFileService _fileService,
                                             IProductRepository _productRepository,
                                             IUnitOfWork _unitOfWork,
                                             IValidator<UpdateProductCommand> validator) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<Product>();

            if (request.File1 != null && request.File1.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl1))
                {
                    await _fileService.DeleteFileAsync(request.ImageUrl1);
                }
                mappedItem.ImageUrl1 = await _fileService.UploadFile(request.File1);
            }

            if (request.File2 != null && request.File2.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl2))
                {
                    await _fileService.DeleteFileAsync(request.ImageUrl2);
                }
                mappedItem.ImageUrl2 = await _fileService.UploadFile(request.File2);
            }

            if (request.mainFile != null && request.mainFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.MainImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.MainImageUrl);
                }
                mappedItem.MainImageUrl = await _fileService.UploadFile(request.mainFile);
            }

            _productRepository.Update(mappedItem);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
