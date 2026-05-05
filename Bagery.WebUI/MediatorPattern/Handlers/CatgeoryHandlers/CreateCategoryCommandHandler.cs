using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class CreateCategoryCommandHandler(ICategoryRepository _categoryRepository
                                             ,IUnitOfWork _unitOfWork
                                             ,IValidator<CreateCategoryCommand> validator) : IRequestHandler<CreateCategoryCommand>
    {
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }



            var item = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(item);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
