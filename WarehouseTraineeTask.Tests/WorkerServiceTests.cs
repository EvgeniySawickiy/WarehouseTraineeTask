using Moq;

using WarehouseTraineeTask.Application.Services;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using Xunit;

namespace WarehouseTraineeTask.Tests
{
    public class WorkerServiceTests
    {
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly WorkerService _workerService;

        public WorkerServiceTests()
        {
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _workerService = new WorkerService(_workerRepositoryMock.Object, _departmentRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddWorkerAndAssignDepartments()
        {
            // Arrange
            var worker = new Worker
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Position = "Manager",
                HireDate = DateTime.UtcNow
            };
            var departmentIds = new List<Guid> { Guid.NewGuid() };

            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Department { Id = departmentIds[0], Name = "Department A" });

            // Act
            await _workerService.AddAsync(worker, departmentIds);

            // Assert
            _workerRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Worker>(w => w.WorkerDepartments.Count == 1)), Times.Once);
            _departmentRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllWorkers()
        {
            // Arrange
            var workers = new List<Worker>
            {
                new Worker { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Worker { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" }
            };

            _workerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(workers);

            // Act
            var result = await _workerService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _workerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnWorkerById()
        {
            // Arrange
            var workerId = Guid.NewGuid();
            var worker = new Worker { Id = workerId, FirstName = "John", LastName = "Doe" };

            _workerRepositoryMock.Setup(repo => repo.GetByIdAsync(workerId)).ReturnsAsync(worker);

            // Act
            var result = await _workerService.GetByIdAsync(workerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(workerId, result.Id);
            _workerRepositoryMock.Verify(repo => repo.GetByIdAsync(workerId), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateWorker()
        {
            // Arrange
            var workerId = Guid.NewGuid();
            var worker = new Worker { Id = workerId, FirstName = "John", LastName = "Doe" };

            // Act
            await _workerService.UpdateAsync(workerId, worker);

            // Assert
            _workerRepositoryMock.Verify(repo => repo.Update(It.Is<Worker>(w => w.Id == workerId)), Times.Once);
        }
    }
}