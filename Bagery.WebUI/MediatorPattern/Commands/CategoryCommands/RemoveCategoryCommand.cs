using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;

public class RemoveCategoryCommand(Guid Id):IRequest
{

    public Guid id { get; set; } = Id;

}
