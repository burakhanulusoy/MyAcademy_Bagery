using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactCommands
{
    public class UpdateContactCommand:IRequest
    {
        public Guid Id { get; set; }
        public string? OpeningHour { get; set; }
        public string? ClosingHour { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MapUrl { get; set; }





    }
}
