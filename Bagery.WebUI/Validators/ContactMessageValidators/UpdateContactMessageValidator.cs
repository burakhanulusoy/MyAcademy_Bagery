using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ContactMessageValidators
{
    public class UpdateContactMessageValidator : AbstractValidator<UpdateContactMessageCommand>
    {
        public UpdateContactMessageValidator()
        {
            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Ad soyad alanı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Ad soyad en az 3 karakter olmalıdır.")
                .MaximumLength(60).WithMessage("Ad soyad en fazla 60 karakter olabilir.")
                .Matches(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$")
                .WithMessage("Ad soyad yalnızca harf içerebilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
                .Matches(@"^(0?5\d{9})$")
                .WithMessage("Telefon numarası 5xxxxxxxxx formatında olmalıdır.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Konu alanı boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Konu en az 5 karakter olmalıdır.")
                .MaximumLength(120).WithMessage("Konu en fazla 120 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj alanı boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.");
        }
    }
}