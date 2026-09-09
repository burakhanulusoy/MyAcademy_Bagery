using Bagery.WebUI.MediatorPattern.Results.ProductResults;

namespace Bagery.WebUI.Services.BageryAi
{
    public class BageryAiResponse
    {
        public bool InScope { get; set; }
        public string Message { get; set; }
        public List<GetProductsQueryResult> Products { get; set; } = [];
    }
}
