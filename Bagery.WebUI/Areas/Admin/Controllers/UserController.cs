using Bagery.WebUI.MediatorPattern.Commands.UserCommands;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Encodings;
using PagedList.Core;
using System.Threading.Tasks;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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


    }
}
