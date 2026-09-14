using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GoogleLoginCommandHandler(SignInManager<AppUser> _signInManager)
        : IRequestHandler<GoogleLoginCommand, AuthenticationProperties>
    {
        public Task<AuthenticationProperties> Handle(
            GoogleLoginCommand request,
            CancellationToken cancellationToken)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                "Google",
                request.RedirectUrl);

            return Task.FromResult(properties);
        }
    }
}