using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CommentCommands;

public record RemoveCommentCommand(Guid Id):IRequest;
