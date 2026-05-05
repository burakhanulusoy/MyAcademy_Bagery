using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Blog:BaseEntity
    {

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl1 { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string LongDescription { get; set; }
        public string MainDescription { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserId { get; set; }
        public IList<Comment> Comments { get; set; }


    }
}
