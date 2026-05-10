using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public bool IsDeleted { get; set; } = false; // =false yapmasan bile false olur default
        public IList<Blog> Blogs { get; set; }
        public IList<Comment> Comments { get; set; }

    }
}
