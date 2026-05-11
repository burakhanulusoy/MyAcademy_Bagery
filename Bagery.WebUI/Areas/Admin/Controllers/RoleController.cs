using Bagery.WebUI.MediatorPattern.Commands.RoleCommands;
using Bagery.WebUI.MediatorPattern.Queries.RoleQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RoleController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetRolesWithUserCountQuery());
            return View(items);
        }

        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _mediator.Send(new RemoveRoleCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> UpdateRole(Guid id)
        {
            var item=await _mediator.Send(new GetRoleByIdWithUserCountQuery(id));
            var mappedItem = item.Adapt<UpdateRoleCommand>();
            return View(mappedItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> GetUserInRole(Guid id)
        {
            var items= await _mediator.Send(new GetUserByRoleIdQuery(id));
            return View(items);

        }












    }
}
