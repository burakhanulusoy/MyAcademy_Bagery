using Bagery.WebUI.Services.BageryAi;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    public class BageryAiController(BageryAiService aiService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(
            [FromBody] BageryAiRequest request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return BadRequest();

            var result = await aiService.AskAsync(
                request.Message.Trim(),
                cancellationToken);

            return Ok(result);
        }
    }
}