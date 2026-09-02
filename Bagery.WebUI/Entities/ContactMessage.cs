using Bagery.WebUI.Entities.Common;
using Bagery.WebUI.Enums;

namespace Bagery.WebUI.Entities
{
    public class ContactMessage:BaseEntity
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public ContactMessageStatus MessageStatus { get; set; }


    }
}
