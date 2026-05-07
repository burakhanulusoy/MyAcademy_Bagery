using Bagery.WebUI.MediatorPattern.Commands.ContactCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ContactValidators
{
    public class CreateContactValidator:AbstractValidator<CreateContactCommand>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.OpeningHour)
                   .NotEmpty().WithMessage("Açılış saati boş bırakılamaz.")
                   .MaximumLength(50).WithMessage("Açılış saati çok uzun olamaz.");

            RuleFor(x => x.ClosingHour)
                .NotEmpty().WithMessage("Kapanış saati boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Kapanış saati çok uzun olamaz.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Lütfen geçerli ve daha uzun bir adres giriniz.")
                .MaximumLength(500).WithMessage("Adres alanı en fazla 500 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi formatı giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Lütfen geçerli bir telefon numarası giriniz.")
                .MaximumLength(20).WithMessage("Telefon numarası çok uzun olamaz.");

            RuleFor(x => x.MapUrl)
                .NotEmpty().WithMessage("Harita URL (MapUrl) alanı boş bırakılamaz.");
        }
    }
}
