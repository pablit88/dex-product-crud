using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Exceptions;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Core.Interfaces.Services;

namespace Dex.ProductCrud.Core.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;


        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository) { 
            _productRepository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> AddAsync(Product product)
        {
            product = await _productRepository.AddAsync(product);

            return product.Id;
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepository.ListAllAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _productRepository.GetByIdAsync(id);

        public async Task UpdateAsync(int productId, Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new NotFoundException("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Image = product.Image;

            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task AddCategoryToProductAsync(int productId, int categoryId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            if (product.Categories.Any(c => c.Id == categoryId))
            {
                throw new ConflictException("Category already set in the product.");
            }

            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            

            product.Categories.Add(category);
            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveCategoryFromProductAsync(int productId, int categoryId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            var category = product.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found in the product.");
            }

            product.Categories.Remove(category);
            await _productRepository.UpdateAsync(product);
        }
    }
}
