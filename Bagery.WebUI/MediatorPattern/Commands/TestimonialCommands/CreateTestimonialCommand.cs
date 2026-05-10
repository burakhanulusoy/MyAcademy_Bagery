using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands
{
    public class CreateTestimonialCommand :IRequest
    {
        public string? Comment { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }



    }
}
