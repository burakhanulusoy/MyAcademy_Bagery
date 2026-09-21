using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Queries.CheckoutQueries;
using Bagery.WebUI.MediatorPattern.Results.CheckoutResults;
using Bagery.WebUI.Services.CartServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.CheckoutHandlers
{
    public class GetCheckoutQueryHandler(UserManager<AppUser> _userManager,
                                         IHttpContextAccessor _httpContextAccessor,
                                         ICartService _cartService) : IRequestHandler<GetCheckoutQuery, GetCheckoutQueryResult>
    {
        public async Task<GetCheckoutQueryResult> Handle(GetCheckoutQuery request, CancellationToken cancellationToken)
        {
            // Giriş yapan kullanıcıyı al (CreateCommentCommandHandler'daki yöntemin aynısı)
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new IdentityException("Ödeme yapmak için giriş yapmalısınız.");

            return new GetCheckoutQueryResult
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,  // IdentityUser'dan gelen hazır alan
                Address = user.Address,          // senin AppUser'a eklediğin alan
                IsCartEmpty = _cartService.GetCart().IsEmpty
            };
        }
    }
}