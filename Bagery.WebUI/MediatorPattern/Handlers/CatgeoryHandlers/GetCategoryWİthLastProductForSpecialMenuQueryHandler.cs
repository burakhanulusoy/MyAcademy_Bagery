using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.Repositories.CategoryRepositories;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CatgeoryHandlers
{
    public class GetCategoryWİthLastProductForSpecialMenuQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategeoryWithLastProductForSpecialMenuQuery,
                          List<GetCategeoryWithLastProductForSpecialMenuQueryResult>>
    {
        public async Task<List<GetCategeoryWithLastProductForSpecialMenuQueryResult>> Handle(
            GetCategeoryWithLastProductForSpecialMenuQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.GetCategoriesWithLastProductForSpecialMenuAsync();

            return categories.Select(c =>
            {
                var last = c.Products.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

                return new GetCategeoryWithLastProductForSpecialMenuQueryResult
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    Product = last == null ? null : new LastProductResult
                    {
                        Id = last.Id,
                        ProductName = last.ProductName,
                        Price = last.Price
                    }
                };
            }).ToList();
        }
    }
}