using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.Exceptions
{
    public class IdentityException(IEnumerable<IdentityError> errors) :Exception("Identity hatası alındı.")
    {

        public IEnumerable<IdentityError> Errors { get; } = errors;

    }
}
