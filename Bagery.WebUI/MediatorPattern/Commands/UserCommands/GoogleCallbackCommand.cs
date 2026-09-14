using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public record GoogleCallbackCommand(string? RemoteError)
        : IRequest<IList<string>>;
}