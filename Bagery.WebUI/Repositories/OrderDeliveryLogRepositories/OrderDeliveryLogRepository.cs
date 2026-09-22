using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.OrderDeliveryLogRepositories
{
    public class OrderDeliveryLogRepository(AppDbContext context) : GenericRepository<OrderDeliveryLog>(context), IOrderDeliveryLogRepository
    {
    }
}