using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using Bagery.WebUI.Repositories.ProductVariantRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductVariantHandlers
{
    public class CreateProductVariantCommandHandler(IProductVariantRepository _productVariantRepository,
                                                    IUnitOfWork _unitOfWork,
                                                    IValidator<CreateProductVariantCommand> _validator) : IRequestHandler<CreateProductVariantCommand>
    {
        public async Task Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {

            var mapped = request.Adapt<ProductVariant>();
            mapped.IsAvailable = true;
            await _productVariantRepository.CreateAsync(mapped);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
