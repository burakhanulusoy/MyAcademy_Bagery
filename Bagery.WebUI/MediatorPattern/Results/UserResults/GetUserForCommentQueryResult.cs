namespace Bagery.WebUI.MediatorPattern.Results.UserResults
{
    public class GetUserForCommentQueryResult
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Job { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}
