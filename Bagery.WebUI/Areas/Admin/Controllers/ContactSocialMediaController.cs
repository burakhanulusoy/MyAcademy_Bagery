using Bagery.WebUI.MediatorPattern.Commands.ContactSocialMediaCommands;
using Bagery.WebUI.MediatorPattern.Queries.ContactSocialMediaQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]



    public class ContactSocialMediaController(IMediator mediator) : Controller
    {
        public IActionResult CreateContactSocialMedia(Guid Id)
        {
            ViewBag.ContactId = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactSocialMedia(CreateContactSocialMediaCommand command)
        {
            await mediator.Send(command);
            return RedirectToAction("Index", "Contact", new { area = "Admin" });

        }

        public async Task<IActionResult> UpdateContactSocialMedia(Guid id)
        {
        
            var item = await mediator.Send(new GetContactSocailMediaByIdQuery(id));
            var mappedItem = item.Adapt<UpdateContactSocialMediaCommand>();
            return View(mappedItem);    


        }
        [HttpPost]
        public async Task<IActionResult> UpdateContactSocialMedia(UpdateContactSocialMediaCommand command)
        {
            
            await mediator.Send(command);
            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteContactSocialMedia(Guid id)
        {
            await mediator.Send(new RemoveContactSocialMediaCommand(id));

            return RedirectToAction("Index", "Contact", new { area = "Admin" });


        }



    }
}
