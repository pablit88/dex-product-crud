using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dex.ProductCrud.Infrastructure.Repositories
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductCrudDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<Product?> GetByIdAsync(int id) => await _dbContext.Set<Product>().Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);

        public new async Task<IEnumerable<Product>> ListAllAsync() => await _dbContext.Set<Product>().Include(p => p.Categories).ToListAsync();
    }
}
