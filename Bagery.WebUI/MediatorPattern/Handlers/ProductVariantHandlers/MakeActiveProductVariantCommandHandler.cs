using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class MakeActiveProductVariantCommandHandler(IProductVariantRepository _productVariantRepository
                                                        ,IUnitOfWork _unitOfWork) : IRequestHandler<MakeActiveProductVariantCommand>
    {
        public async Task Handle(MakeActiveProductVariantCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _productVariantRepository.GetByIdAsync(request.Id);
            item.IsAvailable = true;
            _productVariantRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
