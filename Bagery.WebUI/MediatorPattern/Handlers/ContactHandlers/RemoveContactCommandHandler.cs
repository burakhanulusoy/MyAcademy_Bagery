using Bagery.WebUI.MediatorPattern.Commands.ContactCommands;
using Bagery.WebUI.Repositories.ContactRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactHandlers
{
    public class RemoveContactCommandHandler(IUnitOfWork _unitOfWork,
                                            IContactRepository _contactRepository) : IRequestHandler<RemoveContactCommand>
    {
        public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {

            var item = await _contactRepository.GetByIdAsync(request.Id);
            _contactRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
