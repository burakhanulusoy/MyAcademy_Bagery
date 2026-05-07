using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ContactCommands;
using Bagery.WebUI.Repositories.ContactRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactHandlers
{
    public class CreateContactCommandHandler(IUnitOfWork _unitOfWork,
                                            IContactRepository _contactRepository,
                                            IValidator<CreateContactCommand> validator) : IRequestHandler<CreateContactCommand>
    {
        public async Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var item = request.Adapt<Contact>();
            await _contactRepository.CreateAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
