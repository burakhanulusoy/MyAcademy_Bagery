using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Services.CartServices;
using FluentValidation;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class AddToCartCommandHandler(IProductRepository _productRepository,
                                         ICartService _cartService,
                                         IValidator<AddToCartCommand> _validator) : IRequestHandler<AddToCartCommand, Cart>
    {
        public async Task<Cart> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            // 1) Adet ve ürün Id kontrolü (senin diğer handler'larındaki kalıp)
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            // 2) Ürünü veritabanından al. Bu metot zaten ProductRepository'nde var (varyantlarla birlikte getiriyor).
            var product = await _productRepository.GetProductByIdWithProductVariants(request.ProductId)
                          ?? throw new BusinessException("Ürün bulunamadı.");

            // 3) Seçenek seçildiyse gerçekten o ürüne ait ve satışta mı?
            var variant = request.VariantId is null
                ? null
                : product.ProductVariants?.FirstOrDefault(x => x.Id == request.VariantId && x.IsAvailable)
                  ?? throw new BusinessException("Seçilen seçenek artık mevcut değil.");

            // 4) Sepete ekle. Fiyatı istemciden değil buradan hesaplıyoruz.
            var cart = _cartService.GetCart();

            cart.AddItem(new CartItem
            {
                ProductId = product.Id,
                VariantId = variant?.Id,
                ProductName = product.ProductName,
                VariantName = variant?.OptionName,
                ImageUrl = product.MainImageUrl,
                UnitPrice = product.Price + (variant?.AdditionalPrice ?? 0), // seçenek yoksa ek fiyat 0
                Quantity = request.Quantity
            });

            _cartService.SaveCart(cart);
            return cart;
        }
    }
}