using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.Exceptions
{
    
    public class IdentityException(IEnumerable<IdentityError> errors, string message = "Identity hatası alındı.") : Exception(message)
    {
        public IEnumerable<IdentityError> Errors { get; } = errors;

        public IdentityException(string message) : this(new List<IdentityError>(), message)
        {
        }
    }
}