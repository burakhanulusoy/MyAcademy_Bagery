using FluentValidation.Results;

namespace Bagery.WebUI.Exceptions
{
    public class ValidationUIException(IEnumerable<ValidationFailure> errors) : Exception("Validasyon Hatası alındı.")
    {

        public IEnumerable<ValidationFailure> Errors { get; } = errors;
     


    }

   



}
