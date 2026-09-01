using Bagery.WebUI.MediatorPattern.Commands.CategoryCommands;
using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
   [Authorize(Roles ="Admin")]

    public class CategoryController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetCategoriesQuery());
            return View(items);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _mediator.Send(new RemoveCategoryCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var item = await _mediator.Send(new GetCategoryByIdQuery(id));
            var updateItem = item.Adapt<UpdateCategoryCommand>();
            return View(updateItem);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetCategoryWithProjects(Guid id)
        {
            var items=await  _mediator.Send(new GetProductsByCategoryIdQuery(id));
            
            return View(items);
        }



    }
}
