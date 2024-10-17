using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;

namespace WarehouseTraineeTask.Infrastructure.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
    {
        WarehouseContext _warehouseContext;
        public WorkerRepository(WarehouseContext warehouseContext) : base(warehouseContext) 
        {
            _warehouseContext = warehouseContext;
        }


        public new async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await _warehouseContext.Workers
                .Include(w => w.WorkerDepartments).ToListAsync();
        }
        public new async Task<Worker> GetByIdAsync(Guid id)
        {
            return await _warehouseContext.Workers
                .Include(w => w.WorkerDepartments).FirstOrDefaultAsync(w=>w.Id==id);
        }
    }
}