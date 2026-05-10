using Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.TestimonialValidators
{
    public class UpdateTestimonialValidator:AbstractValidator<UpdateTestimonialCommand>
    {
        public UpdateTestimonialValidator()
        {
            RuleFor(x => x.FullName)
               .NotEmpty().WithMessage("Ad Soyad alanı boş geçilemez.")
               .MinimumLength(3).WithMessage("Ad Soyad en az 3 karakter olmalıdır.")
               .MaximumLength(50).WithMessage("Ad Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ünvan alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ünvan alanı en az 2 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("Ünvan alanı en fazla 30 karakter olabilir.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Yorum alanı boş geçilemez.")
                .MinimumLength(10).WithMessage("Yorum en az 10 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");
        }
    }
}
