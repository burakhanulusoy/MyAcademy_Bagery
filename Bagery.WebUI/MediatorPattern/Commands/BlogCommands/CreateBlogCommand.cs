using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BlogCommands;

public record CreateBlogCommand(
    string Title,
    string ShortDescription,
    string MainDescription,
    IFormFile BackgroundFile,
    string LongTitle,
    string LongDescription,
    IFormFile File1,
    string LastDescription,
    string LastDescriptionTitle) : IRequest;