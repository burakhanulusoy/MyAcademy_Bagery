using Bagery.WebUI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bagery.WebUI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationUIException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            else if (context.Exception is IdentityException exception)
            {
                if (exception.Errors != null && exception.Errors.Any())
                {
                    foreach (var error in exception.Errors)
                    {
                        context.ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else if (!string.IsNullOrEmpty(exception.Message))
                {
                    context.ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            else
            {
                return;
            }

            // --- AJAX / fetch isteği ise view yerine JSON dön ---
            // Contact Message Create işlemi için yapaıldı 


            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var messages = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result = new JsonResult(new
                {
                    success = false,
                    message = string.Join(" ", messages)
                });
                context.ExceptionHandled = true;
                return;
            }
            // --- normal sayfa isteği: mevcut davranış ---




            var actionName = context.RouteData.Values["action"]?.ToString();
            var modelMetadataProvider = context.HttpContext.RequestServices.GetRequiredService<IModelMetadataProvider>();

            context.Result = new ViewResult
            {
                ViewName = actionName,
                ViewData = new ViewDataDictionary(modelMetadataProvider, context.ModelState)
            };
            context.ExceptionHandled = true;
        }
    }
}