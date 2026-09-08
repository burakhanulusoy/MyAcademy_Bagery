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
    public class CreateBlogCommandHandler(IValidator<CreateBlogCommand> validator,
                                          IBlogRepository _blogRepository,
                                          IUnitOfWork _unitOfWork,
                                          IFileService _fileService,
                                          IHttpContextAccessor _httpContextAccessor,
                                          UserManager<AppUser> _userManager) : IRequestHandler<CreateBlogCommand>
    {
        public async Task Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);



            var mappedItem = request.Adapt<Blog>();

            mappedItem.AppUserId = user.Id;
            mappedItem.BackgroundImageUrl = await _fileService.UploadFile(request.BackgroundFile);
            mappedItem.ImageUrl1 = await _fileService.UploadFile(request.File1);

            await _blogRepository.CreateAsync(mappedItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}