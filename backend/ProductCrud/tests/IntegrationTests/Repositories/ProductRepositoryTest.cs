using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Infrastructure.Repositories;
using GenFu;

namespace Dex.ProductCrud.IntegrationTests.Repositories
{
    public class ProductRepositoryTest : BaseRepositoryTest<Product>
    {
        public ProductRepositoryTest(): base()
        {
            _asyncRepository = new ProductRepository(_context);
        }

        protected override Product GetEntity()
        {
            A.Configure<Product>()
                .Fill(c => c.Id, 0);

            return A.New<Product>();
        }

        protected override void ModifyEntity(Product entity)
        {
            entity.Name = "Heineken";
        }
    }
}
