using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.ContactMessageValidators
{
    public class UpdateContactMessageValidator : AbstractValidator<UpdateContactMessageCommand>
    {
        public UpdateContactMessageValidator()
        {
            
        }
    }
}