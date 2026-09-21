using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.CouponValidators
{
    public class UpdateCouponValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Geçersiz kupon.");

            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("Kupon kodu boş bırakılamaz.")
                .Matches(@"^[A-Za-z0-9]{3,30}$").WithMessage("Kupon kodu 3-30 karakter olmalı; sadece harf (Türkçe karakter olmadan) ve rakam içerebilir.");

            RuleFor(x => x.CouponPrice)
                .NotNull().WithMessage("İndirim tutarı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("İndirim tutarı 0'dan büyük olmalı.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                .WithMessage("Minimum sepet tutarı negatif olamaz.");
        }
    }
}