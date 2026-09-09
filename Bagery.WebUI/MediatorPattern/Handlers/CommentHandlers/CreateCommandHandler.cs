using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using Bagery.WebUI.Repositories.CommentRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class CreateCommentCommandHandler(
        ICommentRepository _commentRepository,
        IUnitOfWork _unitOfWork,
        IHttpContextAccessor _httpContextAccessor,
        UserManager<AppUser> _userManager,
        IValidator<CreateCommentCommand> _validator,
        CommentModerationService _moderationService)
        : IRequestHandler<CreateCommentCommand>
    {
        public async Task Handle(
            CreateCommentCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult =
                await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            var user = await _userManager.GetUserAsync(
                _httpContextAccessor.HttpContext!.User);

            if (user is null)
                throw new IdentityException(
                    "Yorum yapmak için giriş yapmalısınız.");

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin") &&
                !roles.Contains("Writer"))
                throw new IdentityException(
                    "Maalesef bloglara sadece yazarlar yorum yapabilir.");

            if (await _moderationService.IsToxicAsync(
                    request.CommentContent,
                    cancellationToken))
                throw new ContentModerationException(
                    "Yorumunuz topluluk kurallarına uygun değil.");

            var comment = request.Adapt<Comment>();
            comment.AppUserId = user.Id;

            await _commentRepository.CreateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}