using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CommentCommands;

public record UpdateCommentCommand(Guid Id,string CommentContent):IRequest;
