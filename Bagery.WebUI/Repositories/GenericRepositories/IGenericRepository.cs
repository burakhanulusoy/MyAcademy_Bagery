using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Repositories.GenericRepositories
{
    public interface IGenericRepository<TEntity> where TEntity :BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueryable(); 
        Task<TEntity> GetByIdAsync(Guid id);
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

    }
}
