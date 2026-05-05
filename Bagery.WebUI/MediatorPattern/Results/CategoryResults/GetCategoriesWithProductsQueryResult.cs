using Bagery.WebUI.MediatorPattern.Results.ProductResults;

namespace Bagery.WebUI.MediatorPattern.Results.CategoryResults
{
    public class GetCategoriesWithProductsQueryResult
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public IList<GetProductsByCategoryQueryResult> Products { get; set; }
    }
}
