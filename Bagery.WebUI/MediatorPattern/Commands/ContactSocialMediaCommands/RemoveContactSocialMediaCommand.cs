using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands
{
    public class RemoveContactSocialMediaCommand(Guid id):IRequest
    {
        public Guid  Id { get; set; } = id;
    }
}
