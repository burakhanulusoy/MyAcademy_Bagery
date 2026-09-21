using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.PaymentValidators
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ad soyad en fazla 100 karakter olabilir.");

            // + ile başlayabilir, rakam ve boşluk içerebilir: "0555 555 55 55" veya "+905555555555"
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
                .Matches(@"^\+?[0-9 ]{10,16}$").WithMessage("Geçerli bir telefon numarası girin.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Teslimat adresi boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalı.")
                .MaximumLength(400).WithMessage("Adres en fazla 400 karakter olabilir.");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Sipariş notu en fazla 500 karakter olabilir.");

            RuleFor(x => x.CardOwner)
                .NotEmpty().WithMessage("Kart üzerindeki isim boş bırakılamaz.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Kart numarası boş bırakılamaz.")
                .Must(BeValidCardNumber).WithMessage("Kart numarası 15-19 haneli olmalı.");

            RuleFor(x => x.ExpiryMonth)
                .NotEmpty().WithMessage("Son kullanma ayı boş bırakılamaz.")
                .Must(m => int.TryParse(m, out var month) && month is >= 1 and <= 12)
                .WithMessage("Son kullanma ayı 01-12 arasında olmalı.");

            RuleFor(x => x.ExpiryYear)
                .NotEmpty().WithMessage("Son kullanma yılı boş bırakılamaz.")
                .Matches(@"^\d{2}$").WithMessage("Son kullanma yılını 2 haneli girin (ör. 30).");

            RuleFor(x => x.Cvv)
                .NotEmpty().WithMessage("CVV boş bırakılamaz.")
                .Matches(@"^\d{3,4}$").WithMessage("CVV 3 veya 4 haneli olmalı.");

            RuleFor(x => x.InstallmentCount)
                .InclusiveBetween(0, 12).WithMessage("Geçersiz taksit seçimi.");
        }

        // Boşlukları sayma: "9792 0303 9444 0796" -> 16 rakam
        private static bool BeValidCardNumber(string? cardNumber)
        {
            var digits = new string((cardNumber ?? string.Empty).Where(char.IsDigit).ToArray());
            return digits.Length is >= 15 and <= 19;
        }
    }
}