using Dex.ProductCrud.Api.Controllers;
using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Services;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dex.ProductCrud.FunctionalTests.Api.Controllers
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productController;

        public ProductControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetProducts_When_Called_Returns_products()
        {
            var products = new List<Product>
            {
                A.New<Product>()
            };

            _productServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            var result = await _productController.Get();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProduct_When_Exists_Returns_Product()
        {
            var product = A.New<Product>();

            _productServiceMock.Setup(s => s.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var result = await _productController.Get(product.Id);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProduct_When_Not_Exists_Returns_NotFound()
        {
            int nonExistingProductId = 1;
            Product? nullProduct = null;

            _productServiceMock.Setup(s => s.GetByIdAsync(nonExistingProductId)).ReturnsAsync(nullProduct);

            var result = await _productController.Get(nonExistingProductId);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_When_Called_With_Valid_Info_Returns_OkResult()
        {
            var newProductInfo = A.New<ProductInfo>();

            int newProductId = 1;
            _productServiceMock.Setup(s => s.AddAsync(newProductInfo)).ReturnsAsync(newProductId);

            var result = await _productController.Post(newProductInfo);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Put_When_Called_With_Valid_Info_Returns_NoContent()
        {
            var existingProductInfo = A.New<ProductInfo>();

            int existingProductId = 1;
            _productServiceMock.Setup(s => s.UpdateAsync(existingProductId, existingProductInfo)).Returns(Task.CompletedTask);

            var result = await _productController.Put(existingProductId, existingProductInfo);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_When_Called_With_Valid_Info_Returns_NoContent()
        {
            int existingProductId = 1;
            _productServiceMock.Setup(s => s.DeleteAsync(existingProductId)).Returns(Task.CompletedTask);

            var result = await _productController.Delete(existingProductId);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task AddCategoryToProduct_When_Called_With_Valid_Info_Returns_NoContent()
        {
            int existingProductId = 1;
            int existingCategoryId = 1;
            _productServiceMock.Setup(s => s.AddCategoryToProductAsync(existingProductId, existingCategoryId)).Returns(Task.CompletedTask);

            var result = await _productController.AddCategoryToProduct(existingProductId, existingCategoryId);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoveCategoryFromProduct_When_Called_With_Valid_Info_Returns_NoContent()
        {
            int existingProductId = 1;
            int existingCategoryId = 1;
            _productServiceMock.Setup(s => s.RemoveCategoryFromProductAsync(existingProductId, existingCategoryId)).Returns(Task.CompletedTask);

            var result = await _productController.RemoveCategoryFromProduct(existingProductId, existingCategoryId);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
