namespace Bagery.WebUI.MediatorPattern.Results.CategoryResults
{
    public class GetCategeoryWithLastProductForSpecialMenuQueryResult
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public LastProductResult Product { get; set; }
    }
    public class LastProductResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
