using Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Testimonial
{
    public class _GetTestimonialsQueryResultQueryResult(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var results = await _mediator.Send(new GetTestimonialsQuery());
            return View(results);

        }



    }
}
