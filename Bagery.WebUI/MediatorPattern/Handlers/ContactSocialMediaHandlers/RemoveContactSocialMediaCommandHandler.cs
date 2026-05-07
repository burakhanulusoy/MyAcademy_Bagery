using Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands;
using Bagery.WebUI.Repositories.ContactSocialMediaRepositories;
using Bagery.WebUI.Services;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactSocialMediaHandlers
{
    public class RemoveContactSocialMediaCommandHandler(IContactSocialMediaRepository contactSocialMediaRepository,
                                                        IUnitOfWork unitOfWork,
                                                        IFileService fileService) : IRequestHandler<RemoveContactSocialMediaCommand>
    {
        public async Task Handle(RemoveContactSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var item = await contactSocialMediaRepository.GetByIdAsync(request.Id);
            await fileService.DeleteFileAsync(item.IconUrl);
            contactSocialMediaRepository.Delete(item);
            await unitOfWork.SaveChangesAsync();   
        }
    }
}
