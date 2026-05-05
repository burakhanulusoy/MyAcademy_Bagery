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
    public class CreateProductCommandHandler(IFileService _fileService,
                                             IProductRepository _productRepository,
                                             IUnitOfWork _unitOfWork,
                                             IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mappedItem = request.Adapt<Product>();
            mappedItem.ImageUrl1 = await _fileService.UploadFile(request.File1);
            mappedItem.ImageUrl2 = await _fileService.UploadFile(request.File2);
            mappedItem.MainImageUrl = await _fileService.UploadFile(request.mainFile);


            await _productRepository.CreateAsync(mappedItem);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
