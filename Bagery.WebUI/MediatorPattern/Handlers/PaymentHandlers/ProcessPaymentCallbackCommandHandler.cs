using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Services.PayTRServices;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PaymentHandlers
{
    public class ProcessPaymentCallbackCommandHandler(IPayTRService _payTRService,
                                                      IOrderRepository _orderRepository,
                                                      IUnitOfWork _unitOfWork,
                                                      ILogger<ProcessPaymentCallbackCommandHandler> _logger) : IRequestHandler<ProcessPaymentCallbackCommand, string>
    {
        private const string Ok = "OK"; // PayTR tam olarak bu metni bekliyor

        public async Task<string> Handle(ProcessPaymentCallbackCommand request, CancellationToken cancellationToken)
        {
            // 1) ÖNCE HASH: istek gerçekten PayTR'den mi geldi? (Adım 6'daki metot)
            if (!_payTRService.IsCallbackHashValid(request.MerchantOid, request.Status, request.TotalAmount, request.Hash))
            {
                _logger.LogWarning("PayTR callback hash uyuşmadı. OrderNo: {OrderNo}", request.MerchantOid);
                return "PAYTR notification failed: bad hash"; // "OK" değil: sipariş güncellenmedi
            }

            // 2) Siparişi bul (Adım 2'deki tracking'li metot: değiştirip kaydedeceğiz)
            var order = await _orderRepository.GetByOrderNoAsync(request.MerchantOid);
            if (order is null)
            {
                // Tekrar denemek bir şey değiştirmez; logla ve OK dön ki PayTR boşuna tekrar göndermesin
                _logger.LogWarning("PayTR callback: sipariş bulunamadı. OrderNo: {OrderNo}", request.MerchantOid);
                return Ok;
            }

            // 3) Bu bildirim daha önce işlendiyse bir şey yapma (PayTR aynı bildirimi tekrar gönderebilir)
            if (order.Status != OrderStatus.Pending)
                return Ok;

            // 4) Sonucu siparişe yaz
            if (request.Status == "success")
            {
                order.Status = OrderStatus.Paid;
                order.StatusMessage = "Ödeme başarılı.";
                order.PaidAt = DateTime.UtcNow; // PostgreSQL: Now değil UtcNow (Adım 1)
            }
            else
            {
                order.Status = OrderStatus.Failed;
                order.StatusMessage = string.IsNullOrWhiteSpace(request.FailedReasonMsg)
                    ? "Ödeme başarısız."
                    : $"{request.FailedReasonMsg} (Kod: {request.FailedReasonCode})"; // Failure sayfasında gösterilecek
            }

            _orderRepository.Update(order);        // senin GenericRepository'deki Update
            await _unitOfWork.SaveChangesAsync();  // UpdatedAt'i interceptor'ın dolduruyor

            _logger.LogInformation("PayTR callback işlendi. OrderNo: {OrderNo}, Durum: {Status}", order.OrderNo, order.Status);

            // 5) success da failed da olsa, işlendiyse "OK"
            return Ok;
        }
    }
}