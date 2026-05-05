using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using FluentValidation;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace Bagery.WebUI.Validators.BannerValidators
{
    public class UpdateBannerValidator:AbstractValidator<UpdateBannerCommand>
    {
        public UpdateBannerValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
              .NotNull().WithMessage("Başlık alanı zorunludur.")
              .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .NotNull().WithMessage("Açıklama alanı zorunludur.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

          
        }
    }
}
