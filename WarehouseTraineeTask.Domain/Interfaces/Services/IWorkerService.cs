using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Domain.Interfaces.Services
{
    public interface IWorkerService
    {
        Task<IEnumerable<Worker>> GetAllAsync();
        Task<Worker> GetByIdAsync(Guid id);
        Task AddAsync(Worker worker, List<Guid>? departmentId);
        Task UpdateAsync(Guid id, Worker worker);
        Task DeleteAsync(Guid id);
    }
}