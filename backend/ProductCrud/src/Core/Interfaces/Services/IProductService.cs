using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;

namespace Dex.ProductCrud.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<int> AddAsync(ProductInfo productInfo);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(int productId, ProductInfo productInfo);
        Task DeleteAsync(int productId);
        Task AddCategoryToProductAsync(int productId, int categoryId);
        Task RemoveCategoryFromProductAsync(int productId, int categoryId);
    }
}
