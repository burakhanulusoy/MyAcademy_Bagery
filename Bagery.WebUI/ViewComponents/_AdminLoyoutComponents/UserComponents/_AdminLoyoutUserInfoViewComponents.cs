using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents._AdminLoyoutComponents.UserComponents
{
    public class _AdminLoyoutUserInfoViewComponents(IMediator _mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _mediator.Send(new GetUserForFobiaTemplateQuery());
            return View(user);
        }
    }
}
