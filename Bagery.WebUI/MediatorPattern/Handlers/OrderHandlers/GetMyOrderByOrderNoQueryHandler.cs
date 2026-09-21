using Bagery.WebUI.Entities;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.OrderHandlers
{
    public class GetMyOrderByOrderNoQueryHandler(IOrderRepository _orderRepository,
                                                 UserManager<AppUser> _userManager,
                                                 IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetMyOrderByOrderNoQuery, GetMyOrderQueryResult?>
    {
        public async Task<GetMyOrderQueryResult?> Handle(GetMyOrderByOrderNoQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OrderNo))
                return null;

            // Giriş yapanın Id'si (metin olarak gelir, Guid'e çeviriyoruz)
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext!.User);
            if (!Guid.TryParse(userId, out var appUserId))
                return null;

            // Adım 2'deki metot: sipariş numarası + kullanıcı Id'si birlikte aranıyor
            var order = await _orderRepository.GetUserOrderWithItemsAsync(request.OrderNo, appUserId);

            // ?. -> sipariş null ise Adapt hiç çalışmaz, null döner
            return order?.Adapt<GetMyOrderQueryResult>();
        }
    }
}