using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands
{
    public class UpdateTestimonialCommand :IRequest
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }

    }
}
