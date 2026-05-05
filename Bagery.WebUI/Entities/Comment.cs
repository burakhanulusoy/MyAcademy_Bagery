using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Comment:BaseEntity
    {
        public string CommentContent { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserId { get; set; }
        public Blog Blog { get; set; }
        public Guid BlogId { get; set; }



    }
}
