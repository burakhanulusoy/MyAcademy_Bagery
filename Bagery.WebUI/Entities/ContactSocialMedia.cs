using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class ContactSocialMedia:BaseEntity
    {
        public string IconUrl { get; set; }
        public string SocialMediaName { get; set; }
        public string SocialMediaUrl  { get; set; }
        public Contact Contact { get; set; }
        public Guid ContactId { get; set; }


    }
}
