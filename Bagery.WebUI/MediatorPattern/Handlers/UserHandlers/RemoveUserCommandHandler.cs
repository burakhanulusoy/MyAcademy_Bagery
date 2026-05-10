using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
    {
        public Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
