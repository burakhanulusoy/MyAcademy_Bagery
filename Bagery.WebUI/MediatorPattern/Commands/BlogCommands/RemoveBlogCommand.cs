using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.BlogCommands;

public record RemoveBlogCommand(Guid Id):IRequest;