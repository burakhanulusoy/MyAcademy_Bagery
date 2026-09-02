using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Controllers
{
    public class ProductController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return View(result);
        }
    }
}
