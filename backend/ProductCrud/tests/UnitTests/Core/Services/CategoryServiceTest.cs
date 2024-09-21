using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Exceptions;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Core.Interfaces.Services;
using Dex.ProductCrud.Core.Services;
using GenFu;
using Moq;

namespace Dex.ProductCrud.UnitTests.Core.Services
{
    public class CategoryServiceTest
    {
        private readonly ICategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public CategoryServiceTest()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService( _categoryRepositoryMock.Object );
        }

        [Fact]
        public async Task AddAsync_Category_Is_Created()
        {
            var categoryInfo = A.New<CategoryInfo>();

            var category = new Category();
            category.Name = categoryInfo.Name;

            _categoryRepositoryMock.Setup(r=> r.AddAsync(It.IsAny<Category>()))
                .Callback(() => category.Id = 1)
                .ReturnsAsync(category);

            var categoryId = await _categoryService.AddAsync(categoryInfo);

            Assert.Equal(1, categoryId);
            Assert.Equal(1, category.Id);
        }

        [Fact]
        public async Task GetByIdAsync_When_Category_Exists_Returns_Category()
        {
            var category = A.New<Category>();

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            var result = await _categoryService.GetByIdAsync(category.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdAsync_When_Category_Not_Exists_Returns_Null()
        {
            var category = A.New<Category>();

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            var result = await _categoryService.GetByIdAsync(category.Id + 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_When_There_Are_Categories_All_Are_Returned()
        {
            int caregoriesCount = 10;
            var categories = new List<Category>();

            for (int i = 0; i < caregoriesCount; i++)
            {
                categories.Add(A.New<Category>());
            }

            _categoryRepositoryMock.Setup(r => r.ListAllAsync())
                .ReturnsAsync(categories);

            var result = await _categoryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.Count() == caregoriesCount);
        }

        [Fact]
        public async Task UpdateAsync_When_Category_Exists_Category_Is_Updated()
        {
            var categoryInfo = A.New<CategoryInfo>();

            int existingId = 1;
            A.Configure<Category>()
                .Fill(c=> c.Id , existingId);
            var category = A.New<Category> ();

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingId))
                .ReturnsAsync(category);

            _categoryRepositoryMock.Setup(r => r.UpdateAsync(category));

            var exception = await Record.ExceptionAsync(() => _categoryService.UpdateAsync(existingId, categoryInfo));

            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateAsync_When_Category_Does_Not_Exist_Throws_NotFoundException()
        {
            int nonExistingId = 1;
            var categoryInfo = A.New<CategoryInfo>();
            Category? nullCategory = null;

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(nullCategory);

            var exception = await Record.ExceptionAsync(() => _categoryService.UpdateAsync(nonExistingId, categoryInfo));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }

        [Fact]
        public async Task DeleteAsync_When_Category_Exists_Category_Is_Deleted()
        {
            var categoryInfo = A.New<CategoryInfo>();

            int existingId = 1;
            A.Configure<Category>()
                .Fill(c => c.Id, existingId);
            var category = A.New<Category>();

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingId))
                .ReturnsAsync(category);

            _categoryRepositoryMock.Setup(r => r.DeleteAsync(category));

            var exception = await Record.ExceptionAsync(() => _categoryService.DeleteAsync(existingId));

            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteAsync_When_Category_Does_Not_Exist_Throws_NotFoundException()
        {
            int nonExistingId = 1;
            var categoryInfo = A.New<CategoryInfo>();
            Category? nullCategory = null;

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(nullCategory);

            var exception = await Record.ExceptionAsync(() => _categoryService.DeleteAsync(nonExistingId));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }
    }
}
