using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class CreateCouponCommandHandler(ICouponRepository _couponRepository,
                                            IUnitOfWork _unitOfWork,
                                            IValidator<CreateCouponCommand> _validator) : IRequestHandler<CreateCouponCommand>
    {
        public async Task Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            // Veritabanında hep BÜYÜK harf (arama da büyük harfle yapılıyor, Adım 2)
            var code = request.CouponCode!.Trim().ToUpperInvariant();

            // Aynı kod varsa hatayı kod kutusunun altında göster
            if (await _couponRepository.CodeExistsAsync(code))
                throw new ValidationUIException([new ValidationFailure(nameof(request.CouponCode), "Bu kupon kodu zaten var.")]);

            await _couponRepository.CreateAsync(new Coupon
            {
                CouponCode = code,
                CouponPrice = request.CouponPrice!.Value,
                MinPrice = request.MinPrice ?? 0,
                IsActive = request.IsActive
            });

            await _unitOfWork.SaveChangesAsync();
        }
    }
}