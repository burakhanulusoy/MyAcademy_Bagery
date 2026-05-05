using Bagery.WebUI.MediatorPattern.Commands.ProductCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ProductValidators
{
    public class CreateProductValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Fiyat alanı boş geçilemez.")
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.ProductName)
                 .NotEmpty().WithMessage("Ürün adı boş geçilemez.")
                 .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır.")
                 .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olmalıdır.");

          
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Ürün açıklaması boş geçilemez.")
                .MinimumLength(10).WithMessage("Açıklama çok kısa, en az 10 karakter yazmalısın.")
                .MaximumLength(1000).WithMessage("Açıklama çok uzun, en fazla 1000 karakter olabilir.");

            RuleFor(x => x.PreperationDescription)
                .NotEmpty().WithMessage("Hazırlanış açıklaması boş geçilemez.")
                .MinimumLength(10).WithMessage("Hazırlanış detayı en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Hazırlanış detayı en fazla 1000 karakter olabilir.");

          
            // Dosyalar (Sadece doluluk kontrolü)
            RuleFor(x => x.mainFile).NotNull().WithMessage("Ana dosya seçilmelidir.");
            RuleFor(x => x.File1).NotNull().WithMessage("Dosya 1 seçilmelidir.");
            RuleFor(x => x.File2).NotNull().WithMessage("Dosya 2 seçilmelidir.");

         

        }
    }
}
