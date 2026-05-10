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
            
            if(context.Exception is ValidationUIException validationException)
            {

                foreach(var error in validationException.Errors)
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
                // 2. Durum: Sadece string olarak fırlattığın düz mesaj ("Şifreler uyuşmuyor" vs.)
                else if (!string.IsNullOrEmpty(exception.Message))
                {
                    context.ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            else
            {
                return;
            }

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
