using Bagery.WebUI.MediatorPattern.Results.CategoryResults;

namespace Bagery.WebUI.MediatorPattern.Results.ProductResults
{
    public class GetProductsQueryResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string PreperationDescription { get; set; }
        public GetCategoriesQueryResult  Category { get; set; }

    }
}
