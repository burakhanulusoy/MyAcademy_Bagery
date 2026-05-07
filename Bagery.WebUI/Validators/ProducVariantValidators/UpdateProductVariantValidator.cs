using Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ProducVariantValidators
{
    public class UpdateProductVariantValidator : AbstractValidator<UpdateProductVariantCommand>
    {
        public UpdateProductVariantValidator()
        {
            RuleFor(x => x.OptionName)
              .NotEmpty().WithMessage("Varyasyon adı boş bırakılamaz!")
              .MinimumLength(2).WithMessage("Varyasyon adı en az 2 karakter olmalıdır!")
              .MaximumLength(100).WithMessage("Varyasyon adı 100 karakterden uzun olamaz!");

            RuleFor(x => x.AdditionalPrice)
                .NotNull().WithMessage("Eklenecek fiyat boş bırakılamaz!")
                .GreaterThanOrEqualTo(0).WithMessage("Eklenecek fiyat 0'dan küçük olamaz!");





        }
    }
}
