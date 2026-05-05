using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class RemoveCategoryCommandHandler(IUnitOfWork _unitOfWork
                                              ,ICategoryRepository _categoryRepository) : IRequestHandler<RemoveCategoryCommand>
    {
        public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var item = await _categoryRepository.GetByIdAsync(request.id);
            _categoryRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
