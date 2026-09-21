using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OrderHandlers
{
    public class GetAdminOrderDetailQueryHandler(IOrderRepository _orderRepository) : IRequestHandler<GetAdminOrderDetailQuery, GetAdminOrderDetailQueryResult?>
    {
        public async Task<GetAdminOrderDetailQueryResult?> Handle(GetAdminOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailForAdminAsync(request.OrderNo);
            return order?.Adapt<GetAdminOrderDetailQueryResult>(); // bulunamazsa null
        }
    }
}