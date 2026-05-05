using FluentValidation.Results;

namespace Bagery.WebUI.Exceptions
{
    public class ValidationException(IEnumerable<ValidationFailure> errors) : Exception("Validasyon Hatası alındı.")
    {

        public IEnumerable<ValidationFailure> Errors { get; } = errors;
        public ValidationException(string errorMessage)
            : this(new[] { new ValidationFailure("General", errorMessage) })
        {
        }



    }

   



}
