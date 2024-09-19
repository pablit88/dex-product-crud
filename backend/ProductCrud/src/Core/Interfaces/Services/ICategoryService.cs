using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;

namespace Dex.ProductCrud.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<int> AddAsync(CategoryInfo categoryInfo);
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task UpdateAsync(int categoryId, CategoryInfo categoryInfo);
        Task DeleteAsync(int categoryId);
    }
}
