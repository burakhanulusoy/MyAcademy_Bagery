using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands
{
    public class UpdateContactSocialMediaCommand:IRequest
    {
        public Guid Id { get; set; }
        public string? IconUrl { get; set; }
        public string? SocialMediaName { get; set; }
        public string? SocialMediaUrl { get; set; }
        public IFormFile? File { get; set; }

        public Guid ContactId { get; set; }
    }
}
