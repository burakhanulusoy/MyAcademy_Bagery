using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class LogoutUserHandler(SignInManager<AppUser> signInManager) : IRequestHandler<LogoutUserCommand>
    {
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();

        }
    }
}
