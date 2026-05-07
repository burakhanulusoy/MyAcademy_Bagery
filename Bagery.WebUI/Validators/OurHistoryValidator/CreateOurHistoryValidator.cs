using Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.OurHistoryValidator;

public class CreateOurHistoryValidator:AbstractValidator<CreateOurHistoryCommand>
{
    public CreateOurHistoryValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama (Hikayemiz) alanı boş bırakılamaz.")
            .MinimumLength(20).WithMessage("Lütfen en az 20 karakterlik daha detaylı bir açıklama giriniz.");

        RuleFor(x => x.SignatureFile)
            .NotNull().WithMessage("Lütfen bir imza görseli seçiniz.");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Lütfen hikaye için bir alt görsel seçiniz.");

        RuleFor(x => x.MainFile)
            .NotNull().WithMessage("Lütfen hikaye için bir ana görsel seçiniz.");
    }


}
