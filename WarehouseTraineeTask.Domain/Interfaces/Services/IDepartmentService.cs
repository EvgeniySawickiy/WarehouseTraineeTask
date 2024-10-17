using WarehouseTraineeTask.Domain.Entity;


namespace WarehouseTraineeTask.Domain.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(Guid id);
        Task<Department> AddAsync(Department department);
        Task<Department> GetByIdWithProductsAsync(Guid id);
        Task UpdateAsync(Guid id, Department department);
        Task DeleteAsync(Guid id);
    }
}