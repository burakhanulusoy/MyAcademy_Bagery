using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;

namespace Bagery.WebUI.MediatorPattern.Results.ContactResults
{
    public class GetContactsWithSocialMediaQueryResult
    {
        public Guid Id  { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MapUrl { get; set; }
        public IList<GetContactSocialMediaByContactQueryResult> ContactSocialMedias { get; set; }


    }
}
