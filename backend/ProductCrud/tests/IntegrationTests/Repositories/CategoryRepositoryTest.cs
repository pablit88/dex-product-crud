
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Infrastructure.Repositories;
using GenFu;

namespace Dex.ProductCrud.IntegrationTests.Repositories
{
    public class CategoryRepositoryTest : BaseRepositoryTest<Category>
    {
        public CategoryRepositoryTest(): base()
        {
            _asyncRepository = new CategoryRepository(_context);
        }

        protected override Category GetEntity()
        {
            A.Configure<Category>()
                .Fill(c => c.Id, 0);

            return A.New<Category>();
        }

        protected override void ModifyEntity(Category entity)
        {
            entity.Name = "Beer";
        }
    }
}
