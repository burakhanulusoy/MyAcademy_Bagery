using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;

public class UpdateCategoryCommand :IRequest
{
    public Guid Id { get; set; }
    public string? CategoryName { get; set; }

}
