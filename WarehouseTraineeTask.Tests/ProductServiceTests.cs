using Moq;
using WarehouseTraineeTask.Application.Services;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using Xunit;

namespace WarehouseTraineeTask.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Product>> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IRepository<Product>>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product A", Quantity = 10, Price = 100 },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Quantity = 20, Price = 200 }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            Assert.Equal(products.Count, result.Count());
            _productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenIdIsValid()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Product A", Quantity = 10, Price = 100 };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewProduct()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", Quantity = 15, Price = 150 };
            _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            await _productService.AddAsync(newProduct);

            // Assert
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Id != Guid.Empty)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updatedProduct = new Product { Id = productId, Name = "Updated Product", Quantity = 25, Price = 250 };

            // Act
            await _productService.UpdateAsync(productId, updatedProduct);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Update(It.Is<Product>(p => p.Id == productId)), Times.Once);
        }

    }
}
