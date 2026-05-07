using Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ContactSocialMediaValidators
{
    public class CreateContactSocialMediaValiator:AbstractValidator<CreateContactSocialMediaCommand>
    {
        public CreateContactSocialMediaValiator()
        {

            RuleFor(x => x.SocialMediaName)
                 .NotEmpty().WithMessage("Sosyal medya adı boş bırakılamaz.")
                 .MaximumLength(50).WithMessage("Sosyal medya adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.SocialMediaUrl)
                .NotEmpty().WithMessage("Sosyal medya bağlantı adresi (URL) boş bırakılamaz.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("Lütfen bir ikon dosyası seçiniz.");

            

        }
    }
}
