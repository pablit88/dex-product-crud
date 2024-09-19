using Dex.ProductCrud.Core.Entities;

namespace Dex.ProductCrud.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<int> AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(int productId, Product product);
        Task DeleteAsync(int productId);
        Task AddCategoryToProductAsync(int productId, int categoryId);
        Task RemoveCategoryFromProductAsync(int productId, int categoryId);
    }
}
