using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class BlogController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Detail(Guid Id)
        {
            var blog = await mediator.Send(new GetBlogByIdQuery(Id));
            return View(blog);
        }
    }
}
