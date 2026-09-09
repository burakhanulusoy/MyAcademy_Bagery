using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CommentCommands;

public record CreateCommentCommand(
                                    string CommentContent, Guid BlogId
                                    ) :IRequest;
