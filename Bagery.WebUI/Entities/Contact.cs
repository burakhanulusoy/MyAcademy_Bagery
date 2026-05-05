using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Contact:BaseEntity
    {
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MapUrl { get; set; }
        public IList<ContactSocialMedia> ContactSocialMedias { get; set; }
    }
}
