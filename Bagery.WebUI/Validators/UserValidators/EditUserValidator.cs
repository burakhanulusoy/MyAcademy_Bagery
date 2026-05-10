using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class EditUserValidator:AbstractValidator<EditUserCommand>
    {
        public EditUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad alanı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Ad Soyad en az 3 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ad Soyad en fazla 50 karakter olabilir.");



            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
         

            

            RuleFor(x => x.FacebookUrl)
                .NotEmpty().WithMessage("Facebook bağlantısı boş bırakılamaz.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Lütfen geçerli bir Facebook bağlantısı giriniz.");

            RuleFor(x => x.TwitterUrl)
                .NotEmpty().WithMessage("Twitter bağlantısı boş bırakılamaz.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Lütfen geçerli bir Twitter bağlantısı giriniz.");

            RuleFor(x => x.InstagramUrl)
                .NotEmpty().WithMessage("Instagram bağlantısı boş bırakılamaz.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Lütfen geçerli bir Instagram bağlantısı giriniz.");

            RuleFor(x => x.Job)
                .NotEmpty().WithMessage("Meslek bilgisi boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Meslek bilgisi en fazla 50 karakter olabilir.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres bilgisi boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Adres bilgisi en fazla 250 karakter olabilir.");

            RuleFor(x => x.AboutMe)
                .NotEmpty().WithMessage("Hakkımda kısmı boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Hakkımda kısmı en fazla 500 karakter olabilir.");
        }
    }
}
