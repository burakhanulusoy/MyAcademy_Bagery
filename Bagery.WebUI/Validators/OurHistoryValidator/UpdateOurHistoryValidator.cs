using Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.OurHistoryValidator
{
    public class UpdateOurHistoryValidator:AbstractValidator<UpdateOurHistoryCommand>
    {
        public UpdateOurHistoryValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
               .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama (Hikayemiz) alanı boş bırakılamaz.")
                .MinimumLength(20).WithMessage("Lütfen en az 20 karakterlik daha detaylı bir açıklama giriniz.");
        }
    }
}
