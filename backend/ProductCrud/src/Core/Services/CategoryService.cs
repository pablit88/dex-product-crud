using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Core.Interfaces.Services;

namespace Dex.ProductCrud.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<int> AddAsync(Category category)
        {
            category = await _categoryRepository.AddAsync(category);

            return category.Id;
        }

        public async Task DeleteAsync(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _categoryRepository.ListAllAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _categoryRepository.GetByIdAsync(id);

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
