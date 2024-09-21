using Dex.ProductCrud.Api.Controllers;
using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Services;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dex.ProductCrud.FunctionalTests.Api.Controllers
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task GetCategories_When_Called_Returns_Categories()
        {
            var categories = new List<Category>
            {
                A.New<Category>()
            };

            _categoryServiceMock.Setup(s=> s.GetAllAsync()).ReturnsAsync(categories);

            var result = await _categoryController.Get();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCategory_When_Exists_Returns_Category()
        {
            var category = A.New<Category>();

            _categoryServiceMock.Setup(s => s.GetByIdAsync(category.Id)).ReturnsAsync(category);

            var result = await _categoryController.Get(category.Id);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCategory_When_Not_Exists_Returns_NotFound()
        {
            int nonExistingCategoryId = 1;
            Category? nullCategory = null;

            _categoryServiceMock.Setup(s => s.GetByIdAsync(nonExistingCategoryId)).ReturnsAsync(nullCategory);

            var result = await _categoryController.Get(nonExistingCategoryId);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_When_Called_With_Valid_Info_Returns_OkResult()
        {
            var newCategoryInfo = A.New<CategoryInfo>();

            int newCategoryId = 1;
            _categoryServiceMock.Setup(s => s.AddAsync(newCategoryInfo)).ReturnsAsync(newCategoryId);

            var result = await _categoryController.Post(newCategoryInfo);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Put_When_Called_With_Valid_Info_Returns_NoContent()
        {
            var existingCategoryInfo = A.New<CategoryInfo>();

            int existingCategoryId = 1;
            _categoryServiceMock.Setup(s => s.UpdateAsync(existingCategoryId, existingCategoryInfo)).Returns(Task.CompletedTask);

            var result = await _categoryController.Put(existingCategoryId, existingCategoryInfo);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_When_Called_With_Valid_Info_Returns_NoContent()
        {
            int existingCategoryId = 1;
            _categoryServiceMock.Setup(s => s.DeleteAsync(existingCategoryId)).Returns(Task.CompletedTask);

            var result = await _categoryController.Delete(existingCategoryId);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

    }
}
