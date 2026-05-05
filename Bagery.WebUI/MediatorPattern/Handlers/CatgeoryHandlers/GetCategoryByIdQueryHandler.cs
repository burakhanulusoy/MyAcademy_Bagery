using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.Repositories.CategoryRepositories;
using Mapster;
using MediatR;
using Microsoft.CodeAnalysis.Operations;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class GetCategoryByIdQueryHandler(ICategoryRepository _categoryRepository) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
    {
        public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
           var item = await _categoryRepository.GetByIdAsync(request.Id);
           return item.Adapt<GetCategoryByIdQueryResult>();


        }
    }
}
