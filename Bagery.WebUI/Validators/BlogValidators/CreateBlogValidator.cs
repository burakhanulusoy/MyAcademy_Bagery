using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.BlogValidators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Blog başlığı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Blog başlığı en fazla 150 karakter olabilir.");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Kısa açıklama boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Kısa açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.MainDescription)
                .NotEmpty().WithMessage("Ana açıklama boş bırakılamaz.");

            RuleFor(x => x.BackgroundFile)
                .NotEmpty().WithMessage("Arka plan görseli boş bırakılamaz.");

            RuleFor(x => x.LongTitle)
                .NotEmpty().WithMessage("Detay başlığı boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Detay başlığı en fazla 200 karakter olabilir.");

            RuleFor(x => x.LongDescription)
                .NotEmpty().WithMessage("Detay açıklaması boş bırakılamaz.");

            RuleFor(x => x.File1)
                .NotEmpty().WithMessage("Blog görseli boş bırakılamaz.");

            RuleFor(x => x.LastDescriptionTitle)
                .NotEmpty().WithMessage("Son açıklama başlığı boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Son açıklama başlığı en fazla 200 karakter olabilir.");

            RuleFor(x => x.LastDescription)
                .NotEmpty().WithMessage("Son açıklama boş bırakılamaz.");

          

          
        }
    }
}