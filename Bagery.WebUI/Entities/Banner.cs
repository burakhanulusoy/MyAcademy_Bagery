using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Banner:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundImageUrl { get; set; }
    }
}
