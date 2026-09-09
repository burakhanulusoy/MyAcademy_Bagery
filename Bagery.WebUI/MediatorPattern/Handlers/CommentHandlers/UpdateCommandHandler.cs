using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using Bagery.WebUI.Repositories.CommentRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class UpdateCommentCommandHandler(ICommentRepository _commentRepository,
                                             IUnitOfWork _unitOfWork,
                                             IHttpContextAccessor _httpContextAccessor,
                                             UserManager<AppUser> _userManager,
                                             IValidator<UpdateCommentCommand> _validator) : IRequestHandler<UpdateCommentCommand>
    {
        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            

            var comment = await _commentRepository.GetByIdAsync(request.Id);

            if (comment is null)
                throw new IdentityException("Yorum bulunamadı.");

            // Yorumu sadece sahibi düzenleyebilir. Admin dahil kimse başkasınınkine dokunamaz.
            if (comment.AppUserId != user.Id)
                throw new IdentityException("Yorum bulunamadı.");

            comment.CommentContent = request.CommentContent;

            _commentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}