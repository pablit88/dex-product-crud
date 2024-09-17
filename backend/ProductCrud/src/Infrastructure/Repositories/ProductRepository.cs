using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Infrastructure.Data;

namespace Dex.ProductCrud.Infrastructure.Repositories
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductCrudDbContext dbContext) : base(dbContext)
        {
        }
    }
}
