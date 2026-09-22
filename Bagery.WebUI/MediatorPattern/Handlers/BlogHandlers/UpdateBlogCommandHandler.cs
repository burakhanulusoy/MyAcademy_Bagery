using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using Bagery.WebUI.Repositories.BlogRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class UpdateBlogCommandHandler(IBlogRepository _blogRepository,
                                          IFileService _fileService,
                                          IUnitOfWork _unitOfWork,
                                          IHttpContextAccessor _httpContextAccessor,
                                          UserManager<AppUser> _userManager,
                                          IValidator<UpdateBlogCommand> _validator) : IRequestHandler<UpdateBlogCommand>
    {
        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new IdentityException("Bu işlem için giriş yapmalısınız.");

            // YENİ: blogun gerçek sahibini oku (AsNoTracking: aşağıdaki Update ile çakışmaz)
            var existing = await _blogRepository.GetBlogByIdWithUser(request.Id)
                           ?? throw new IdentityException("Blog bulunamadı.");

            // YENİ: admin her blogu, yazar sadece kendi blogunu düzenler
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin && existing.AppUserId != user.Id)
                throw new IdentityException("Blog bulunamadı.");

            var mappedBlog = request.Adapt<Blog>();
            mappedBlog.AppUserId = existing.AppUserId; // DEĞİŞTİ: admin düzenlese bile yazar değişmesin

            if (request.BackgroundImageFile != null && request.BackgroundImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.BackgroundImageUrl))
                {
                    await _fileService.DeleteFileAsync(request.BackgroundImageUrl);
                }
                mappedBlog.BackgroundImageUrl = await _fileService.UploadFile(request.BackgroundImageFile);
            }

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl1))
                {
                    await _fileService.DeleteFileAsync(request.ImageUrl1);
                }
                mappedBlog.ImageUrl1 = await _fileService.UploadFile(request.ImageFile);
            }

            _blogRepository.Update(mappedBlog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}