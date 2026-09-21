using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.OrderRepositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<Order?> GetByOrderNoAsync(string orderNo);
        Task<Order?> GetUserOrderWithItemsAsync(string orderNo, Guid appUserId);
        Task<Order?> GetOrderDetailForAdminAsync(string orderNo); // YENİ: kullanıcı filtresi yok, hesap bilgisiyle
    }
}
