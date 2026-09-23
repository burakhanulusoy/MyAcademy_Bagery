using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.DeliveryCommands;
using Bagery.WebUI.Repositories.OrderDeliveryLogRepositories;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Services.RealtimeServices;
using Bagery.WebUI.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.DeliveryHandlers
{
    public class UndoDeliveryCommandHandler(IOrderRepository _orderRepository,
                                            IOrderDeliveryLogRepository _logRepository,
                                            IUnitOfWork _unitOfWork,
                                            UserManager<AppUser> _userManager,
                                            IHttpContextAccessor _httpContextAccessor,
                                            IDeliveryNotifier _notifier) : IRequestHandler<UndoDeliveryCommand>
    {
        // Garson yanlış tıklamayı bu süre içinde düzeltebilir; sonrası sadece admin
        public static readonly TimeSpan UndoWindow = TimeSpan.FromMinutes(5);

        public async Task Handle(UndoDeliveryCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByOrderNoAsync(request.OrderNo)
                        ?? throw new BusinessException("Sipariş bulunamadı.");

            if (order.DeliveryStatus != request.From)
                throw new BusinessException("Bu sipariş az önce başka biri tarafından güncellendi. Pano yenileniyor.");

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new BusinessException("Bu işlem için giriş yapmalısınız.");
            var staff = user.FullName ?? user.UserName ?? "Bilinmiyor";
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            // Geri alınacak adımın zamanı
            var lastStepAt = order.DeliveryStatus switch
            {
                DeliveryStatus.OnTheWay => order.DispatchedAt,
                DeliveryStatus.Delivered => order.DeliveredAt,
                _ => null
            };

            if (lastStepAt is null)
                throw new BusinessException("Geri alınacak bir adım yok.");

            if (!isAdmin && DateTime.UtcNow - lastStepAt.Value > UndoWindow)
                throw new BusinessException("Geri alma süresi (5 dakika) doldu. Düzeltme için admin ile görüşün.");

            string action;
            if (order.DeliveryStatus == DeliveryStatus.OnTheWay)
            {
                order.DeliveryStatus = DeliveryStatus.Waiting;
                order.DispatchedAt = null;
                order.DispatchedBy = null;
                action = "Geri alındı: Yola çıktı";
            }
            else
            {
                order.DeliveryStatus = DeliveryStatus.OnTheWay;
                order.DeliveredAt = null;
                order.DeliveredBy = null;
                action = "Geri alındı: Teslim edildi";
            }

            _orderRepository.Update(order);
            await _logRepository.CreateAsync(new OrderDeliveryLog
            {
                OrderId = order.Id,
                Action = action,       // geri alma da kayda geçer: kanıt silinmez
                PerformedBy = staff
            });

            await _unitOfWork.SaveChangesAsync();

            // YENİ: geri alma da ekranlara yansısın
            await _notifier.DeliveryChangedAsync(order.OrderNo, order.DeliveryStatus, order.DispatchedAt, order.DeliveredAt);
        }
    }
}