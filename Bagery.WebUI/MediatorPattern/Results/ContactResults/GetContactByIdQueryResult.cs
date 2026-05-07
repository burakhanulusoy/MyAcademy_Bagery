namespace Bagery.WebUI.MediatorPattern.Results.ContactResults
{
    public class GetContactByIdQueryResult
    {
        public Guid Id { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MapUrl { get; set; }
    }
}
