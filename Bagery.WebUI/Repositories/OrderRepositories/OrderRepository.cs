using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.OrderRepositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext _context) : base(_context)
        {
        }

        // Callback için: siparişi bulup Status'unu değiştireceğiz
        public Task<Order?> GetByOrderNoAsync(string orderNo)
        {
            return _table.FirstOrDefaultAsync(x => x.OrderNo == orderNo);
        }

        // Success sayfası için: sadece okuma, sadece o kullanıcının siparişi
        public Task<Order?> GetUserOrderWithItemsAsync(string orderNo, Guid appUserId)
        {
            return _table.AsNoTracking()
                         .Include(x => x.OrderItems)
                         .FirstOrDefaultAsync(x => x.OrderNo == orderNo && x.AppUserId == appUserId);
        }
        // YENİ: admin detay sayfası için
        public Task<Order?> GetOrderDetailForAdminAsync(string orderNo)
        {
            return _table.AsNoTracking()
                         .Include(x => x.OrderItems)
                         .Include(x => x.AppUser)
                         .Include(x => x.DeliveryLogs) // YENİ: kanıt çizelgesi için
                         .FirstOrDefaultAsync(x => x.OrderNo == orderNo);
        }


    }
}
