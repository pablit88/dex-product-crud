using Dex.ProductCrud.Core.Entities;

namespace Dex.ProductCrud.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<int> AddAsync(Category category);
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task UpdateAsync(int categoryId, Category category);
        Task DeleteAsync(int categoryId);
    }
}
