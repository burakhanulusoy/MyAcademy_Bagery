using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class ToggleCouponStatusCommandHandler(ICouponRepository _couponRepository,
                                                  IUnitOfWork _unitOfWork) : IRequestHandler<ToggleCouponStatusCommand>
    {
        public async Task Handle(ToggleCouponStatusCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.Id)
                         ?? throw new BusinessException("Kupon bulunamadı.");

            coupon.IsActive = !coupon.IsActive; // tersine çevir
            _couponRepository.Update(coupon);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}