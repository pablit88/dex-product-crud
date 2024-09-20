using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Exceptions;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Core.Interfaces.Services;
using Dex.ProductCrud.Core.Services;
using GenFu;
using Moq;

namespace UnitTests.Core.Services
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Product_Is_Created()
        {
            var productInfo = A.New<ProductInfo>();

            var product = new Product();
            product.Name = productInfo.Name;

            _productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Callback(() => product.Id = 1)
                .ReturnsAsync(product);

            var productId = await _productService.AddAsync(productInfo);

            Assert.Equal(1, productId);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task GetByIdAsync_When_Product_Exists_Returns_Product()
        {
            var product = A.New<Product>();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            var result = await _productService.GetByIdAsync(product.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdAsync_When_Product_Not_Exists_Returns_Null()
        {
            var product = A.New<Product>();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            var result = await _productService.GetByIdAsync(product.Id + 1);

            Assert.Null(result);
        }


        [Fact]
        public async Task GetAllAsync_When_There_Are_Products_All_Are_Returned()
        {
            int productsCount = 10;
            var products = new List<Product>();

            for (int i = 0; i < productsCount; i++)
            {
                products.Add(A.New<Product>());
            }

            _productRepositoryMock.Setup(r => r.ListAllAsync())
                .ReturnsAsync(products);

            var result = await _productService.GetAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.Count() == productsCount);
        }

        [Fact]
        public async Task UpdateAsync_When_Product_Exists_Product_Is_Updated()
        {
            var productInfo = A.New<ProductInfo>();

            int existingId = 1;
            A.Configure<Product>()
                .Fill(c => c.Id, existingId);
            var product = A.New<Product>();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(existingId))
                .ReturnsAsync(product);

            _productRepositoryMock.Setup(r => r.UpdateAsync(product));

            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync(existingId, productInfo));

            Assert.Null(exception);
        }


        [Fact]
        public async Task UpdateAsync_When_Product_Does_Not_Exist_Throws_NotFoundException()
        {
            int nonExistingId = 1;
            var productInfo = A.New<ProductInfo>();
            Product? nullProduct = null;

            _productRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(nullProduct);

            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync(nonExistingId, productInfo));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }


        [Fact]
        public async Task DeleteAsync_When_Product_Exists_Product_Is_Deleted()
        {
            var productInfo = A.New<ProductInfo>();

            int existingId = 1;
            A.Configure<Product>()
                .Fill(c => c.Id, existingId);
            var product = A.New<Product>();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(existingId))
                .ReturnsAsync(product);

            _productRepositoryMock.Setup(r => r.DeleteAsync(product));

            var exception = await Record.ExceptionAsync(() => _productService.DeleteAsync(existingId));

            Assert.Null(exception);
        }


        [Fact]
        public async Task DeleteAsync_When_Product_Does_Not_Exist_Throws_NotFoundException()
        {
            int nonExistingId = 1;
            var productInfo = A.New<ProductInfo>();
            Product? nullProduct = null;

            _productRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(nullProduct);

            var exception = await Record.ExceptionAsync(() => _productService.DeleteAsync(nonExistingId));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }

        [Fact]
        public async Task AddCategoryToProductAsync_When_Product_And_Category_Exist_And_Are_Not_Already_Related_Category_Is_Added()
        {
            int existingProductId = 1;
            int existingCategoryId = 1;

            A.Configure<Product>().Fill(p=> p.Id, existingProductId);
            A.Configure<Category>().Fill(c => c.Id, existingCategoryId);

            var product = A.New<Product>();
            var category = A.New<Category>();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(existingProductId))
                .ReturnsAsync(product);

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategoryId))
                .ReturnsAsync(category);

            var exception = await Record.ExceptionAsync(() => _productService.AddCategoryToProductAsync(existingProductId, existingCategoryId));

            Assert.Null(exception);
        }

        [Fact]
        public async Task AddCategoryToProductAsync_When_Product_And_Category_Exist_But_Are_Already_Related_Throws_ConflictException()
        {
            int existingProductId = 1;
            int existingCategoryId = 1;

            A.Configure<Category>().Fill(c => c.Id, existingCategoryId);

            var productCategory = A.New<Category>();

            A.Configure<Product>().Fill(p => p.Id, existingProductId);

            var product = A.New<Product>();
            product.Categories.Add(productCategory);

            _productRepositoryMock.Setup(r => r.GetByIdAsync(existingProductId))
                .ReturnsAsync(product);

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategoryId))
                .ReturnsAsync(productCategory);

            var exception = await Record.ExceptionAsync(() => _productService.AddCategoryToProductAsync(existingProductId, existingCategoryId));

            Assert.NotNull(exception);
            Assert.IsType<ConflictException>(exception);
        }

        [Fact]
        public async Task AddCategoryToProductAsync_When_Product_Does_Not_Exist_Throws_NotFoundException()
        {
            int nonExistingProductId = 1;
            int existingCategoryId = 1;

            A.Configure<Category>().Fill(c => c.Id, existingCategoryId);

            var category = A.New<Category>();

            Product? nullProduct = null;

            _productRepositoryMock.Setup(r => r.GetByIdAsync(nonExistingProductId))
                .ReturnsAsync(nullProduct);

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategoryId))
                .ReturnsAsync(category);

            var exception = await Record.ExceptionAsync(() => _productService.AddCategoryToProductAsync(nonExistingProductId, existingCategoryId));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }

        [Fact]
        public async Task AddCategoryToProductAsync_When_Category_Does_Not_Exist_Throws_NotFoundException()
        {
            int existingProductId = 1;
            int nonExistingCategoryId = 1;

            A.Configure<Product>().Fill(p => p.Id, existingProductId);

            var product = A.New<Product>();

            Category? nullCategory = null;

            _productRepositoryMock.Setup(r => r.GetByIdAsync(existingProductId))
                .ReturnsAsync(product);

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(nonExistingCategoryId))
                .ReturnsAsync(nullCategory);

            var exception = await Record.ExceptionAsync(() => _productService.AddCategoryToProductAsync(existingProductId, nonExistingCategoryId));

            Assert.NotNull(exception);
            Assert.IsType<NotFoundException>(exception);
        }
    }
}
