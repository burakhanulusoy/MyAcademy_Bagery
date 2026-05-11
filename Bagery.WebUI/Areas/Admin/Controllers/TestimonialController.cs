using Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands;
using Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestimonialController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var items = await _mediator.Send(new GetTestimonialsQuery());
            return View(items);
        }

        public async Task<IActionResult> DeleteTestimonial(Guid id)
        {
            await _mediator.Send(new RemoveTestimonialCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateTestimonial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateTestimonial(Guid Id)
        {
            var item = await _mediator.Send(new GetTestimonialByIdQuery(Id));
            var mappedItem = item.Adapt<UpdateTestimonialCommand>();
            return View(mappedItem);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
