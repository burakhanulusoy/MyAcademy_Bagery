using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Bagery.WebUI.UOW;
using MediatR;

public class UpdateContactCommandHandler(IUnitOfWork unitOfWork,
                                         IContactMessageRepository contactMessageRepository)
                                         : IRequestHandler<UpdateContactMessageCommand>
{
    public async Task Handle(UpdateContactMessageCommand request, CancellationToken cancellationToken)
    {
        var item = await contactMessageRepository.GetByIdAsync(request.Id);
       

        item.MessageStatus = request.MessageStatus;

        contactMessageRepository.Update(item);
        await unitOfWork.SaveChangesAsync();
    }
}