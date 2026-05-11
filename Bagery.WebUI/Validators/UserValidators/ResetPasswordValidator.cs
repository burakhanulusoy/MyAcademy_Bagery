using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Şifreler birbiriyle uyuşmuyor.");

        }
    }
}
