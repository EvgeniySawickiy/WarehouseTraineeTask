using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Domain.Interfaces.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}