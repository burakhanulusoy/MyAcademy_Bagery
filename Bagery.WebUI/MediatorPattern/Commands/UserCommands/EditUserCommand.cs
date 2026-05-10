using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public class EditUserCommand:IRequest
    {

        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? file { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? Job { get; set; }
        public string? AboutMe { get; set; }
        public string? Address { get; set; }




    }
}
