using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class UpdateCouponCommandHandler(ICouponRepository _couponRepository,
                                            IUnitOfWork _unitOfWork,
                                            IValidator<UpdateCouponCommand> _validator) : IRequestHandler<UpdateCouponCommand>
    {
        public async Task Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            var code = request.CouponCode!.Trim().ToUpperInvariant();

            // Kendisi hariç başka kuponda bu kod var mı
            if (await _couponRepository.CodeExistsAsync(code, request.Id))
                throw new ValidationUIException([new ValidationFailure(nameof(request.CouponCode), "Bu kupon kodu başka bir kuponda kullanılıyor.")]);

            // Veritabanındaki kaydı alıp alanlarını değiştiriyoruz (CreatedAt vb. korunur)
            var coupon = await _couponRepository.GetByIdAsync(request.Id)
                         ?? throw new BusinessException("Kupon bulunamadı.");

            coupon.CouponCode = code;
            coupon.CouponPrice = request.CouponPrice!.Value;
            coupon.MinPrice = request.MinPrice ?? 0;
            coupon.IsActive = request.IsActive;

            _couponRepository.Update(coupon);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}