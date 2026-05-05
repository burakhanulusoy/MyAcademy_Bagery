using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.CategoryValidators
{
    public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {

            RuleFor(x => x.CategoryName)
                            .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
                            .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                            .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olmalıdır.");
        }
    }
}
