using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BlogCommands;

public record UpdateBlogCommand(Guid Id,
    string Title,
    string ShortDescription,
    string MainDescription,
    string BackgroundImageUrl,
    IFormFile BackgroundImageFile,
    string LongTitle,
    string LongDescription,
    string ImageUrl1,
    IFormFile ImageFile,
    string LastDescription,
    string LastDescriptionTitle):IRequest;
