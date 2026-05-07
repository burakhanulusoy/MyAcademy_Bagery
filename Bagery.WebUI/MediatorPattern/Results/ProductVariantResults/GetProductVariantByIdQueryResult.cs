namespace Bagery.WebUI.MediatorPattern.Results.ProductVariantResults
{
    public class GetProductVariantByIdQueryResult
    {
        public Guid Id { get; set; }
        public string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public Guid ProductId { get; set; }

    }
}
