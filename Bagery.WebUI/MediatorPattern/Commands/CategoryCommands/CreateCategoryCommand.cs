using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;

public class CreateCategoryCommand :IRequest
{
    public string CategoryName { get; set; }
}
