using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.OurHistoryCommands
{
    public class CreateOurHistoryCommand:IRequest
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SignatureUrl { get; set; }
        public IFormFile? SignatureFile { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? File { get; set; }

        public string? MainImageUrl { get; set; }
        public IFormFile? MainFile { get; set; }



    }
}
