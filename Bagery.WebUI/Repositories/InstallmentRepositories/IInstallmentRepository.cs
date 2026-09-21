using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.InstallmentRepositories
{
    public interface IInstallmentRepository:IGenericRepository<Installment>
    {
        Task<List<Installment>> GetByBrandAsync(string brand);
        Task<Installment?> GetAsync(string brand, int installmentCount);
        Task AddRangeAsync(IEnumerable<Installment> installments);
        Task RemoveAllAsync();
    }
}
