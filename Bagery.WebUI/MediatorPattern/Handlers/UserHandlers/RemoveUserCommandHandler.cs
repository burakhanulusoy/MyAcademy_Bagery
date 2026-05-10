using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class RemoveUserCommandHandler(UserManager<AppUser> _userManager) : IRequestHandler<RemoveUserCommand>
    {
        public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            //Tombstoning (Mezar Taşı İşareti)
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            user.IsDeleted = true;

            string deletedStamp = $"_deleted_{Guid.NewGuid()}";

            user.Email = user.Email + deletedStamp;
            user.NormalizedEmail = user.Email.ToUpper();

            user.UserName = user.UserName + deletedStamp;
            user.NormalizedUserName = user.UserName.ToUpper();

            await _userManager.UpdateAsync(user);


        }
    }
}
