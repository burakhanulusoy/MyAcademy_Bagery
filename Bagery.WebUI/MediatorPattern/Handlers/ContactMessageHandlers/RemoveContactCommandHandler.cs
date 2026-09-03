using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactMessageHandlers
{
    public class RemoveContactCommandHandler(IContactMessageRepository _contactMessageRepository,
                                             IUnitOfWork _unitOfWork) : IRequestHandler<RemoveContactMessageCommand>
    {
        public async Task Handle(RemoveContactMessageCommand request, CancellationToken cancellationToken)
        {
           
            var result = await _contactMessageRepository.GetByIdAsync(request.Id);
             _contactMessageRepository.Delete(result);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
