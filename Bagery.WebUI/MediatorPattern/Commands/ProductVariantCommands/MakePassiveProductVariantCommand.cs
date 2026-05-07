using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ProductVariantCommands
{
    public class MakePassiveProductVariantCommand(Guid id) :IRequest
    {
        public Guid Id { get; set; } = id;



    }
}
