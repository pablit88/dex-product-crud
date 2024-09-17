using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Infrastructure.Data;

namespace Dex.ProductCrud.Infrastructure.Repositories
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProductCrudDbContext dbContext) : base(dbContext)
        {
        }
    }
}
