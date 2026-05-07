using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands
{
    public class CreateContactSocialMediaCommand:IRequest
    {
        public string? IconUrl { get; set; }
        public string? SocialMediaName { get; set; }
        public string? SocialMediaUrl { get; set; }
        public IFormFile? File { get; set; }
        public Guid ContactId { get; set; } 

    }
}
