using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class RegisterUserValidator:AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Lütfen adınızı ve soyadınızı giriniz.")
                .MinimumLength(5).WithMessage("Ad soyad en az 5 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ad soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .MinimumLength(3).WithMessage("Kullanıcı adı çok kısa.")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Kullanıcı adı sadece harf, rakam ve alt çizgi içerebilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir e-posta formatı giriniz.");

            // Telefon Numarası Kuralları
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
        }
    }
}
