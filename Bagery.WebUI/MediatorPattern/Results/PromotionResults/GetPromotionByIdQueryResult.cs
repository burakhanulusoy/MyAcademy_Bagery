namespace Bagery.WebUI.MediatorPattern.Results.PromotionResults
{
    public class GetPromotionByIdQueryResult
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public decimal PromotionPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PromotionCode { get; set; }

    }
}
