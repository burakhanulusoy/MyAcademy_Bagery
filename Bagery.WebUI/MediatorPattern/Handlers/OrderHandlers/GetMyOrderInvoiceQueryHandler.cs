using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using Bagery.WebUI.Repositories.ContactRepositories;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Services.InvoiceServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.OrderHandlers
{
    public class GetMyOrderInvoiceQueryHandler(IOrderRepository _orderRepository,
                                               IContactRepository _contactRepository,
                                               IInvoicePdfService _invoicePdfService,
                                               UserManager<AppUser> _userManager,
                                               IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetMyOrderInvoiceQuery, GetMyOrderInvoiceQueryResult?>
    {
        public async Task<GetMyOrderInvoiceQueryResult?> Handle(GetMyOrderInvoiceQuery request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext!.User);
            if (!Guid.TryParse(userId, out var appUserId))
                return null;

            // Adım 2'deki metot: sadece bu kullanıcının siparişi, ürünleriyle
            var order = await _orderRepository.GetUserOrderWithItemsAsync(request.OrderNo, appUserId);

            // Ödenmemiş siparişin faturası olmaz
            if (order is null || order.Status != OrderStatus.Paid)
                return null;

            // Satıcı bilgisi: admin > İletişim Bilgileri (senin mevcut repository metodun)
            var seller = await _contactRepository.GetContactLastAsync();

            return new GetMyOrderInvoiceQueryResult
            {
                Content = _invoicePdfService.Generate(order, seller),
                FileName = $"Bagery-Fatura-{InvoicePdfService.InvoiceNo(order.OrderNo)}.pdf"
            };
        }
    }
}