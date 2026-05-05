using Bagery.WebUI.MediatorPattern.Commands.BannerCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.BannerValidators
{
    public class CreateBannerValidator:AbstractValidator<CreateBannerCommand>
    {
        public CreateBannerValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .NotNull().WithMessage("Başlık alanı zorunludur.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .NotNull().WithMessage("Açıklama alanı zorunludur.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.File1)
                .NotNull().WithMessage("Lütfen birinci görseli seçiniz.")
                .Must(file => file?.Length > 0).WithMessage("Birinci görsel dosyası boş olamaz.");

            RuleFor(x => x.File2)
                .NotNull().WithMessage("Lütfen ikinci görseli seçiniz.")
                .Must(file => file?.Length > 0).WithMessage("İkinci görsel dosyası boş olamaz.");


        }


    }
}
