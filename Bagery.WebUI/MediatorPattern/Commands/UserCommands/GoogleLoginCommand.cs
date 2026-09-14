using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Bagery.WebUI.MediatorPattern.Commands.UserCommands
{
    public record GoogleLoginCommand(string RedirectUrl)
        : IRequest<AuthenticationProperties>;
}


// Bu, Google'a yönlendirmeden önce gereken AuthenticationProperties nesnesini üretecek