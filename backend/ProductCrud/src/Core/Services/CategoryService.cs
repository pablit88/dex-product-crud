using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Exceptions;
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

        public async Task<int> AddAsync(CategoryInfo categoryInfo)
        {
            var category = new Category() {  Name = categoryInfo.Name };
            category = await _categoryRepository.AddAsync(category);

            return category.Id;
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _categoryRepository.ListAllAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _categoryRepository.GetByIdAsync(id);

        public async Task UpdateAsync(int categoryId, CategoryInfo categoryInfo)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new NotFoundException("Category not found.");
            }

            existingCategory.Name = categoryInfo.Name;

            await _categoryRepository.UpdateAsync(existingCategory);
        }
    }
}
