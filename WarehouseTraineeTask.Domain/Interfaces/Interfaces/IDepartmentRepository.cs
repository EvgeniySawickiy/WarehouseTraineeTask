using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Domain.Interfaces.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetByIdWithProductsAsync(Guid id);
    }
}