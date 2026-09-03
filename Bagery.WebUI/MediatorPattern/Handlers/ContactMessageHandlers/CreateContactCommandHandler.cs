using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Bagery.WebUI.Services.EmailServices;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
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

            if(!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var item = request.Adapt<ContactMessage>();

            var sixDigittCode=Random.Shared.Next(100000, 999999).ToString();

            item.VerificationCode = sixDigittCode;
            item.CodeExpireDate = DateTime.UtcNow.AddMinutes(5);
            item.IsVerified = false;

            item.MessageStatus = ContactMessageStatus.Pending;

            await _contactMessageRepository.CreateAsync(item);
            await unitOfWork.SaveChangesAsync();
            await _emailService.SendEmailFor2FactorAuthentication(item.Email, sixDigittCode, item.NameSurname);







        }
    }
}
