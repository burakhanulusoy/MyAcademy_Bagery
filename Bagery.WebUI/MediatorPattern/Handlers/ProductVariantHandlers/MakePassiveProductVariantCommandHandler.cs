using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class MakePassiveProductVariantCommandHandler(IProductVariantRepository _productVariantRepository,
                                                         IUnitOfWork _unitOfWork) : IRequestHandler<MakePassiveProductVariantCommand>
    {
        public async Task Handle(MakePassiveProductVariantCommand request, CancellationToken cancellationToken)
        {

            var item = await _productVariantRepository.GetByIdAsync(request.Id);
            item.IsAvailable = false;
            _productVariantRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
