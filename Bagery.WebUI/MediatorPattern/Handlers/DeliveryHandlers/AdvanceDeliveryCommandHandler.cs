using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;
using Bagery.WebUI.Repositories.OrderDeliveryLogRepositories;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.DeliveryHandlers
{
    public class AdvanceDeliveryCommandHandler(IOrderRepository _orderRepository,
                                               IOrderDeliveryLogRepository _logRepository,
                                               IUnitOfWork _unitOfWork,
                                               UserManager<AppUser> _userManager,
                                               IHttpContextAccessor _httpContextAccessor) : IRequestHandler<AdvanceDeliveryCommand>
    {
        public async Task Handle(AdvanceDeliveryCommand request, CancellationToken cancellationToken)
        {
            // Takipli okuma (Adım 2'deki metot): değişiklikler SaveChanges'te yazılır
            var order = await _orderRepository.GetByOrderNoAsync(request.OrderNo)
                        ?? throw new BusinessException("Sipariş bulunamadı.");

            if (order.Status != OrderStatus.Paid)
                throw new BusinessException("Ödemesi onaylanmamış sipariş yola çıkarılamaz.");

            // Ekrandaki durum ile veritabanındaki farklıysa başkası az önce değiştirmiş
            if (order.DeliveryStatus != request.From)
                throw new BusinessException("Bu sipariş az önce başka biri tarafından güncellendi. Pano yenileniyor.");

            var staff = await CurrentStaffNameAsync();
            var now = DateTime.UtcNow; // PostgreSQL timestamptz: UTC şart
            string action;

            switch (order.DeliveryStatus)
            {
                case DeliveryStatus.Waiting:
                    order.DeliveryStatus = DeliveryStatus.OnTheWay;
                    order.DispatchedAt = now;
                    order.DispatchedBy = staff;
                    action = "Yola çıktı";
                    break;

                case DeliveryStatus.OnTheWay:
                    order.DeliveryStatus = DeliveryStatus.Delivered;
                    order.DeliveredAt = now;
                    order.DeliveredBy = staff;
                    action = "Teslim edildi";
                    break;

                default:
                    throw new BusinessException("Bu sipariş zaten teslim edildi.");
            }

            _orderRepository.Update(order);
            await _logRepository.CreateAsync(new OrderDeliveryLog
            {
                OrderId = order.Id,
                Action = action,
                PerformedBy = staff
            });

            await _unitOfWork.SaveChangesAsync(); // sipariş ve kayıt aynı anda yazılır
        }

        private async Task<string> CurrentStaffNameAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new BusinessException("Bu işlem için giriş yapmalısınız.");
            return user.FullName ?? user.UserName ?? "Bilinmiyor";
        }
    }
}