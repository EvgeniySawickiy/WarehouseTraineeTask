
using Moq;
using WarehouseTraineeTask.Application.Services;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using Xunit;

namespace WarehouseTraineeTask.Tests
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _departmentService = new DepartmentService(_departmentRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department { Id = Guid.NewGuid(), Name = "Department A" },
                new Department { Id = Guid.NewGuid(), Name = "Department B" }
            };
            _departmentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllAsync();

            // Assert
            Assert.Equal(departments.Count, result.Count());
            _departmentRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDepartment_WhenIdIsValid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department { Id = departmentId, Name = "Department A" };
            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(departmentId)).ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetByIdAsync(departmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(departmentId, result.Id);
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewDepartment()
        {
            // Arrange
            var newDepartment = new Department { Name = "New Department", Location = "Location A", Capacity = 100 };
            _departmentRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Department>())).Returns(Task.CompletedTask);

            // Act
            var result = await _departmentService.AddAsync(newDepartment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newDepartment.Name, result.Name);
            _departmentRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Department>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingDepartment()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var updatedDepartment = new Department { Id = departmentId, Name = "Updated Name", Capacity = 150 };

            // Act
            await _departmentService.UpdateAsync(departmentId, updatedDepartment);

            // Assert
            _departmentRepositoryMock.Verify(repo => repo.Update(It.IsAny<Department>()), Times.Once);
        }


        [Fact]
        public async Task GetByIdWithProductsAsync_ShouldReturnDepartmentWithProducts_WhenIdIsValid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department
            {
                Id = departmentId,
                Name = "Department A",
                Products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Quantity = 10, Price = 100 },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Quantity = 20, Price = 200 }
        }
            };
            _departmentRepositoryMock.Setup(repo => repo.GetByIdWithProductsAsync(departmentId)).ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetByIdWithProductsAsync(departmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(departmentId, result.Id);
            Assert.Equal(2, result.Products.Count);
            _departmentRepositoryMock.Verify(repo => repo.GetByIdWithProductsAsync(departmentId), Times.Once);
        }


    }
}