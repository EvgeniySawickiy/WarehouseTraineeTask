
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> RegisterAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
    }
}