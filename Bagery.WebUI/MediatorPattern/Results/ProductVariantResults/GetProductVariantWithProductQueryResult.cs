namespace Bagery.WebUI.MediatorPattern.Results.ProductVariantResults
{
    public class GetProductVariantWithProductQueryResult
    {
        public Guid Id { get; set; }
        public string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsAvailable { get; set; } //hala devam ediyor mu ?
    }
}
