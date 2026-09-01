using Bagery.WebUI.MediatorPattern.Commands.ClientCommands;
using Bagery.WebUI.MediatorPattern.Queries.ClientQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ClientController(IMediator _mediator) : Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await _mediator.Send(new GetClientsQuery());
        return View(items);
    }

    public async Task<IActionResult> DeleteClient(Guid id)
    {
        await _mediator.Send(new RemoveClientCommand(id));
        return RedirectToAction("Index");
    }

    public IActionResult CreateClient()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(CreateClientCommand command)
    {
        await _mediator.Send(command);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateClient(Guid id)
    {
        var item = await _mediator.Send(new GetClientByIdQuery(id));
        var updateItem = item.Adapt<UpdateClientCommand>();
        return View(updateItem);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateClient(UpdateClientCommand command)
    {
        await _mediator.Send(command);
        return RedirectToAction("Index");
    }
}