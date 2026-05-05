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
    public class UpdateCategoryCommandHandler(IUnitOfWork _unitOfWork
                                              , ICategoryRepository _categoryRepository
                                              ,IValidator<UpdateCategoryCommand> validator) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }
            var item = request.Adapt<Category>();
            _categoryRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
