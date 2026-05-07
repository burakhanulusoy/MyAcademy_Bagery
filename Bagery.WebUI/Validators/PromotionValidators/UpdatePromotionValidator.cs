using Bagery.WebUI.MediatorPattern.Commands.PromotionCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.PromotionValidators
{
    public class UpdatePromotionValidator:AbstractValidator<UpdatePromotionCommand>
    {
        public UpdatePromotionValidator()
        {
            RuleFor(x => x.Title)
                          .NotEmpty().WithMessage("Promosyon başlığı boş geçilemez.")
                          .MinimumLength(3).WithMessage("Promosyon başlığı en az 3 karakter olmalıdır.")
                          .MaximumLength(100).WithMessage("Promosyon başlığı en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Promosyon açıklaması boş geçilemez.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");

            RuleFor(x => x.PromotionPrice)
                .NotNull().WithMessage("Promosyon fiyatı belirtilmelidir.")
                .GreaterThan(0).WithMessage("Promosyon fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.PromotionCode)
                .NotEmpty().WithMessage("Promosyon kodu boş geçilemez.")
                .MaximumLength(50).WithMessage("Promosyon kodu en fazla 50 karakter olabilir.");

            
        }
    }
}
