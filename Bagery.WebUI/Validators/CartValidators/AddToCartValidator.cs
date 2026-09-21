using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using FluentValidation;

namespace Bagery.WebUI.Validators.CartValidators
{
    public class AddToCartValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Geçersiz ürün.");

            // Adım 3'teki sabit: sınırı değiştirmek istersen tek yerden (Cart.cs) değişir
            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, Cart.MaxQuantityPerItem)
                .WithMessage($"Adet 1 ile {Cart.MaxQuantityPerItem} arasında olmalı.");
        }
    }
}