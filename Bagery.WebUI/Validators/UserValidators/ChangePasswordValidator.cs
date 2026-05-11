using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NowPassword)
                .NotEmpty().WithMessage("Mevcut şifre alanı boş bırakılamaz.");


            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre alanı boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter uzunluğunda olmalıdır.")
                .NotEqual(x => x.NowPassword).WithMessage("Yeni şifre, mevcut şifreniz ile aynı olamaz.");

            RuleFor(x => x.NewConfirmPassword)
                .NotEmpty().WithMessage("Yeni şifre tekrar alanı boş bırakılamaz.")
                .Equal(x => x.NewPassword).WithMessage("Girdiğiniz şifreler birbiriyle eşleşmiyor.");



        }
    }
}
