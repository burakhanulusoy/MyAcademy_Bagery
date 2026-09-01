using Bagery.WebUI.MediatorPattern.Commands.ClientCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ClientValidators
{
    public class CreateClientValidators : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("İsim en fazla 100 karakter olabilir.");

            RuleFor(x => x.ClientImage)
                .NotNull().WithMessage("Görsel seçmelisiniz.");

            When(x => x.ClientImage != null, () =>
            {
                RuleFor(x => x.ClientImage.Length)
                    .GreaterThan(0).WithMessage("Seçilen dosya boş.")
                    .LessThanOrEqualTo(2 * 1024 * 1024)
                    .WithMessage("Görsel boyutu en fazla 2 MB olabilir.")
                    .WithName("ClientImage");

                RuleFor(x => x.ClientImage.FileName)
                    .Must(fileName =>
                    {
                        var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                        return allowed.Contains(Path.GetExtension(fileName).ToLowerInvariant());
                    })
                    .WithMessage("Sadece jpg, jpeg, png veya webp uzantılı dosya yükleyebilirsiniz.")
                    .WithName("ClientImage");
            });
        }
    }
}