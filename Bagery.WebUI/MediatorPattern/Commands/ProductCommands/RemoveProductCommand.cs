using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ProductCommands
{
    public class RemoveProductCommand(Guid id) : IRequest
    {
        public Guid Id { get; set; } = id;

    }
}
