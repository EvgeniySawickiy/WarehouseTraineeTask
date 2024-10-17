using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Guid id, Product product);
        Task DeleteAsync(Guid id);
    }
}