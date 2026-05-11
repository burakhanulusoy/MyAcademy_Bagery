using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.UserValidators
{
    public class ForgotPasswordValidator:AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş bırakılamaz");

        }
    }
}
