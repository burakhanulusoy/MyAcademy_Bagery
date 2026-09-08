using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class BlogController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var blogs = await _mediator.Send(new GetBlogsWithUserQuery());
            return View(blogs);
        }

        public async Task<IActionResult> MyBlogs()
        {
            var blogs= await _mediator.Send(new GetBlogByUserIdQuery());
            return View(blogs);
        }

        public IActionResult CreateBlog()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
        {
            await _mediator.Send(createBlogCommand);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateBlog(Guid Id)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery(Id));
            return View(blog);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand updateBlogCommand)
        {
            await _mediator.Send(updateBlogCommand);
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> DeleteBlog(Guid Id)
        {
            await _mediator.Send(new RemoveBlogCommand(Id));
            return RedirectToAction(nameof(Index));
        }



    }
}
