namespace Bagery.WebUI.MediatorPattern.Results.UserResults
{
    public class GetUserByIdQueryResult
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }

        public IList<string> Roles { get; set; }

    }
}
