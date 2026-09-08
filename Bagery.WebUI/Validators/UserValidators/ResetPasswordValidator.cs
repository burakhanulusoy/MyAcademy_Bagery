using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Geçersiz bağlantı");
            RuleFor(x => x.Token).NotEmpty().WithMessage("Geçersiz veya süresi dolmuş bağlantı");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş bırakılamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrarı boş bırakılamaz")
                .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor");


        }
    }
}
