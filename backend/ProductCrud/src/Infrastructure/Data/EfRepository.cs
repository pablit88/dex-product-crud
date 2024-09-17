using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dex.ProductCrud.Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ProductCrudDbContext _dbContext;

        public EfRepository(ProductCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ListAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
