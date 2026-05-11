using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class UserController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 10)
        {
            ViewData["CurrentSearch"] = search;

            // Sistemdeki kullanıcıları çekiyoruz
            var users = await _mediator.Send(new GetUsersQuery());

            if (!string.IsNullOrWhiteSpace(search))
            {
                // StringComparison.OrdinalIgnoreCase ile büyük/küçük harf duyarlılığını ortadan kaldırıyoruz
                users = users.Where(u =>
                    (u.FullName != null && u.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (u.Email != null && u.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            var values = new PagedList<GetUsersQueryResult>(users.AsQueryable(), page, pageSize);

            return View(values);
        }

        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return View(user);

        }

        public async Task<IActionResult> RemoveUser(Guid id)
        {
            await _mediator.Send(new RemoveUserCommand(id));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateUser()
        {
            var user = await _mediator.Send(new GetUserByManagerQuery());
            var mappedUser = user.Adapt<EditUserCommand>();
            return View(mappedUser);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            TempData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi.";

            return RedirectToAction(nameof(UpdateUser));

        }


        public async Task<IActionResult> AssignRole(Guid Id)
        {
            var roleList = await _mediator.Send(new GetUserRolesQuery(Id));

            var command = new AssignRoleUserCommand
            {
                Id = Id,
                Roles = roleList.Adapt<List<RoleAssignDto>>() 
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleUserCommand command)
        {

            await _mediator.Send(command);

            return RedirectToAction("GetUserById", "User", new { id = command.Id });


        }



    }
}
