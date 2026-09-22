using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.DeliveryQueries;
using Bagery.WebUI.MediatorPattern.Results.DeliveryResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.DeliveryHandlers
{
    public class GetDeliveryBoardQueryHandler(IOrderRepository _orderRepository) : IRequestHandler<GetDeliveryBoardQuery, GetDeliveryBoardQueryResult>
    {
        public async Task<GetDeliveryBoardQueryResult> Handle(GetDeliveryBoardQuery request, CancellationToken cancellationToken)
        {
            // "Bugün": Türkiye saatine göre bugünün başlangıcı, UTC'ye çevrilmiş
            var todayStartUtc = DateTime.Now.Date.ToUniversalTime();

            // Tek sorgu: ödenmiş ve (henüz teslim edilmemiş YA DA bugün teslim edilmiş) siparişler
            var cards = await _orderRepository.GetQueryable()
                .Where(o => o.Status == OrderStatus.Paid &&
                            (o.DeliveryStatus != DeliveryStatus.Delivered || o.DeliveredAt >= todayStartUtc))
                .Select(o => new
                {
                    o.DeliveryStatus,
                    Card = new DeliveryCard(
                        o.OrderNo,
                        o.FullName,
                        o.PhoneNumber,
                        o.Address,
                        o.Note,
                        o.PaidPrice,
                        o.PaidAt ?? o.CreatedAt,
                        o.DispatchedAt,
                        o.DispatchedBy,
                        o.DeliveredAt,
                        o.DeliveredBy,
                        o.OrderItems.Select(i => new DeliveryCardItem(i.ProductName, i.VariantName, i.Quantity)).ToList())
                })
                .ToListAsync(cancellationToken);

            return new GetDeliveryBoardQueryResult
            {
                ServerTimeUtc = DateTime.UtcNow,
                Waiting = cards.Where(x => x.DeliveryStatus == DeliveryStatus.Waiting)
                               .Select(x => x.Card).OrderBy(c => c.PaidAt).ToList(),
                OnTheWay = cards.Where(x => x.DeliveryStatus == DeliveryStatus.OnTheWay)
                                .Select(x => x.Card).OrderBy(c => c.DispatchedAt).ToList(),
                DeliveredToday = cards.Where(x => x.DeliveryStatus == DeliveryStatus.Delivered)
                                      .Select(x => x.Card).OrderByDescending(c => c.DeliveredAt).Take(30).ToList()
            };
        }
    }
}