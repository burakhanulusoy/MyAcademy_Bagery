using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class UpdateProductVariantCommandHandler(IUnitOfWork _unitOfWork
                                                   ,IProductVariantRepository _productVariantRepository
                                                   ,IValidator<UpdateProductVariantCommand> validator) : IRequestHandler<UpdateProductVariantCommand>
    {
        public async Task Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {

            var item = await _productVariantRepository.GetByIdAsync(request.Id);

            var mappedItem = request.Adapt(item);
            _productVariantRepository.Update(mappedItem);
            await _unitOfWork.SaveChangesAsync();



        }
    }
}
