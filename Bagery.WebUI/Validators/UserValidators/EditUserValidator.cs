using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserValidator()
        {
            // Herkes için zorunlu
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad alanı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Ad Soyad en az 3 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ad Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Değişiklikleri kaydetmek için mevcut şifrenizi girin.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres bilgisi boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Adres bilgisi en fazla 250 karakter olabilir.");

            // DEĞİŞTİ: aşağıdakiler isteğe bağlı (müşterinin sosyal medyası olmak zorunda değil);
            // doldurulursa kurallar yine geçerli
            RuleFor(x => x.Job)
                .MaximumLength(50).WithMessage("Meslek bilgisi en fazla 50 karakter olabilir.");

            RuleFor(x => x.AboutMe)
                .MaximumLength(500).WithMessage("Hakkımda kısmı en fazla 500 karakter olabilir.");

            RuleFor(x => x.FacebookUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.FacebookUrl))
                .WithMessage("Lütfen geçerli bir Facebook bağlantısı giriniz.");

            RuleFor(x => x.TwitterUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.TwitterUrl))
                .WithMessage("Lütfen geçerli bir Twitter bağlantısı giriniz.");

            RuleFor(x => x.InstagramUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.InstagramUrl))
                .WithMessage("Lütfen geçerli bir Instagram bağlantısı giriniz.");
        }

        private static bool BeValidUrl(string? url) => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}