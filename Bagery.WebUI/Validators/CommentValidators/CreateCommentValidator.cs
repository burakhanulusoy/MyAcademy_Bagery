using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using FluentValidation;

namespace Bagery.WebUI.Validators.CommentValidators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.CommentContent)
                .NotEmpty().WithMessage("Yorum boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Yorum en az 5 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");

            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Geçersiz blog.");
        }
    }
}