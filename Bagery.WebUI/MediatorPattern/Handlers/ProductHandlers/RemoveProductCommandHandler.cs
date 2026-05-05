using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class RemoveProductCommandHandler(IFileService _fileService,
                                             IProductRepository _productRepository,
                                             IUnitOfWork _unitOfWork) : IRequestHandler<RemoveProductCommand>
    {
        public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _productRepository.GetByIdAsync(request.Id);

            await _fileService.DeleteFileAsync(item.ImageUrl1);
            await _fileService.DeleteFileAsync(item.ImageUrl2);

            _productRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();
        }



    }
}
