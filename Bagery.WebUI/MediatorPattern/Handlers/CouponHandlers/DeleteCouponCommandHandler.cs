using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class DeleteCouponCommandHandler(ICouponRepository _couponRepository,
                                            IUnitOfWork _unitOfWork) : IRequestHandler<DeleteCouponCommand>
    {
        public async Task Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.Id)
                         ?? throw new BusinessException("Kupon bulunamadı.");

            // Interceptor'ın bunu soft delete yapar (IsDeleted = true).
            // Eski siparişlerde kupon kodu metin olarak saklandığı için siparişler etkilenmez.
            _couponRepository.Delete(coupon);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}