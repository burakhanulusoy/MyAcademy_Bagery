using Bagery.WebUI.MediatorPattern.Results.CategoryResults;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;

namespace Bagery.WebUI.MediatorPattern.Results.ShopPageResults
{
    public class ShopPageResult
    {
        public List<GetProductsByCategoryQueryResult> Products { get; set; } = new();
        public List<GetCategoriesWithProductsQueryResult> Categories { get; set; } = new();
        public Guid? CategoryId { get; set; }
        public string? Search { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }

        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
