using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.InstallmentCommands;
using Bagery.WebUI.Repositories.InstallmentRepositories;
using Bagery.WebUI.Services.PayTRServices;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.InstallmentHandlers
{
    public class UpdateInstallmentRatesCommandHandler(IPayTRService _payTRService,
                                                      IInstallmentRepository _installmentRepository,
                                                      IUnitOfWork _unitOfWork) : IRequestHandler<UpdateInstallmentRatesCommand, int>
    {
        public async Task<int> Handle(UpdateInstallmentRatesCommand request, CancellationToken cancellationToken)
        {
            // 1) ÖNCE PayTR'den al. PayTR'ye ulaşılamazsa servis hata fırlatır ve aşağıya hiç inilmez.
            var rates = await _payTRService.GetInstallmentRatesAsync(cancellationToken);

            // 2) Boş liste geldiyse mevcut oranları silme
            if (rates.Count == 0)
                throw new BusinessException("PayTR'den taksit oranı gelmedi, mevcut oranlar korundu.");

            // 3) Eskileri sil (Adım 2'deki gerçek DELETE)
            await _installmentRepository.RemoveAllAsync();

            // 4) Yenileri ekle. PayTR modelini entity'ye çeviriyoruz.
            await _installmentRepository.AddRangeAsync(rates.Select(x => new Installment
            {
                Brand = x.Brand,
                InstallmentCount = x.InstallmentCount,
                Rate = x.Rate
            }));

            await _unitOfWork.SaveChangesAsync();
            return rates.Count;
        }
    }
}