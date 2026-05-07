namespace Bagery.WebUI.MediatorPattern.Results.OurHistoryResults
{
    public class GetOurHistoriesQueryResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SignatureUrl { get; set; }
        public string ImageUrl { get; set; }
        public string MainImageUrl { get; set; }
    }
}
