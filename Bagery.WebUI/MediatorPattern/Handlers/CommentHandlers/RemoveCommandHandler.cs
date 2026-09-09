using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using Bagery.WebUI.Repositories.CommentRepositories;
using Bagery.WebUI.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.CommentHandlers
{
    public class RemoveCommentCommandHandler(ICommentRepository _commentRepository,
                                             IUnitOfWork _unitOfWork,
                                             IHttpContextAccessor _httpContextAccessor,
                                             UserManager<AppUser> _userManager) : IRequestHandler<RemoveCommentCommand>
    {
        public async Task Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user is null)
                throw new IdentityException("Bu işlem için giriş yapmalısınız.");

            var comment = await _commentRepository.GetByIdAsync(request.Id);

            if (comment is null)
                throw new IdentityException("Yorum bulunamadı.");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            // Admin her yorumu siler, diğerleri sadece kendi yorumunu
            if (!isAdmin && comment.AppUserId != user.Id)
                throw new IdentityException("Yorum bulunamadı.");

            _commentRepository.Delete(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}