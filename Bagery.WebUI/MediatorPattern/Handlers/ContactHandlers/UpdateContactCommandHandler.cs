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
    public class UpdateContactCommandHandler(IUnitOfWork _unitOfWork,
                                            IContactRepository _contactRepository,
                                            IValidator<UpdateContactCommand> validator) : IRequestHandler<UpdateContactCommand>
    {
        public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var item = request.Adapt<Contact>();
            _contactRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
