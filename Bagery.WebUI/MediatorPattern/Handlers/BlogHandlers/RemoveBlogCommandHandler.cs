using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using Bagery.WebUI.Repositories.BlogRepositories;
using Bagery.WebUI.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class RemoveBlogCommandHandler(IBlogRepository _blogRepository,
                                          IUnitOfWork _unitOfWork,
                                          IHttpContextAccessor _httpContextAccessor,
                                          UserManager<AppUser> _userManager) : IRequestHandler<RemoveBlogCommand>
    {
        public async Task Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            // RemoveCommentCommandHandler'daki kuralın aynısı:
            // admin her blogu siler, yazar sadece kendi blogunu
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new IdentityException("Bu işlem için giriş yapmalısınız.");

            var blog = await _blogRepository.GetByIdAsync(request.Id)
                       ?? throw new IdentityException("Blog bulunamadı.");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin && blog.AppUserId != user.Id)
                throw new IdentityException("Blog bulunamadı."); // başkasının blogu: var olduğunu bile belli etme

            _blogRepository.Delete(blog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}