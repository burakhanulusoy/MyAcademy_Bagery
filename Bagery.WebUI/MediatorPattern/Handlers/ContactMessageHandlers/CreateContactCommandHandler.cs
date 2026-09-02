using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Bagery.WebUI.Services.EmailServices;
using Bagery.WebUI.UOW;
using FluentValidation;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactMessageHandlers
{
    public class CreateContactCommandHandler(IContactMessageRepository _contactMessageRepository,
                                             IEmailService _emailService,
                                             IUnitOfWork unitOfWork,
                                             IValidator<CreateContactMessageCommand> validator) : IRequestHandler<CreateContactMessageCommand>
    {
        public async Task Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await validator.ValidateAsync(request);











        }
    }
}
