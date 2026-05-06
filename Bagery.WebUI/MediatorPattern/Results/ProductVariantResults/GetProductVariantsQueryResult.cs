using Bagery.WebUI.MediatorPattern.Results.ProductResults;

namespace Bagery.WebUI.MediatorPattern.Results.ProductVariantResults
{
    public class GetProductVariantsQueryResult
    {
        public Guid Id { get; set; }
        public string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsAvailable { get; set; } //hala devam ediyor mu ?
        public GetProductsQueryResult Product { get; set; }



    }
}
