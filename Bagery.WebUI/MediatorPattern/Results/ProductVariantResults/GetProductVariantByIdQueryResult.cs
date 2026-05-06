namespace Bagery.WebUI.MediatorPattern.Results.ProductVariantResults
{
    public class GetProductVariantByIdQueryResult
    {
        public Guid Id { get; set; }
        public string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsAvailable { get; set; } //hala devam ediyor mu ?
        public Guid ProductId { get; set; }
    }
}
