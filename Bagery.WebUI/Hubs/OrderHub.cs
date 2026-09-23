using Bagery.WebUI.Repositories.OrderRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Bagery.WebUI.Hubs
{
    // Canlı sipariş bildirimleri. Tarayıcılar buraya bağlanır, sunucu onlara haber gönderir.
    [Authorize] // giriş yapmayan bağlanamaz
    public class OrderHub(IOrderRepository _orderRepository) : Hub
    {
        public const string StaffGroup = "staff";                       // garson + admin
        public static string OrderGroup(string orderNo) => $"order-{orderNo}"; // tek siparişi izleyen müşteri

        public override async Task OnConnectedAsync()
        {
            // Personel ekranları (garson panosu, admin takip) baştan gruba alınır
            if (Context.User is not null && (Context.User.IsInRole("Waiter") || Context.User.IsInRole("Admin")))
                await Groups.AddToGroupAsync(Context.ConnectionId, StaffGroup);

            await base.OnConnectedAsync();
        }

        // Müşteri sipariş detayını açtığında çağırır: "şu siparişi izlemek istiyorum"
        public async Task WatchOrder(string orderNo)
        {
            var userId = CurrentUserId();

            // Sahiplik kontrolü: sorgu siparişi sadece sahibine döndürür, başkasınınsa null gelir
            var order = await _orderRepository.GetUserOrderWithItemsAsync(orderNo, userId);
            if (order is null)
                throw new HubException("Bu sipariş size ait değil.");

            await Groups.AddToGroupAsync(Context.ConnectionId, OrderGroup(orderNo));
        }

        private Guid CurrentUserId()
        {
            var id = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(id, out var userId) ? userId : throw new HubException("Giriş yapmalısınız.");
        }
    }
}