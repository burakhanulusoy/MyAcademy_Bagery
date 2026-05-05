
using Bagery.WebUI.Context;
using Bagery.WebUI.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bagery.WebUI.Repositories.GenericRepositories
{
    public class GenericRepository<TEntity>(AppDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly DbSet<TEntity> _table = _context.Set<TEntity>();

        public async Task CreateAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
           _context.Remove(entity);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _table.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _table.AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
    }
}
