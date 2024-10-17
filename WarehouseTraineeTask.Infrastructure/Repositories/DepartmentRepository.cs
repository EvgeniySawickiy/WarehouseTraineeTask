using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;

namespace WarehouseTraineeTask.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        WarehouseContext _warehouseContext;
        public DepartmentRepository(WarehouseContext warehouseContext) : base(warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task<Department> GetByIdWithProductsAsync(Guid id)
        {
            return await _context.Departments
                .Include(d => d.Products) 
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public new async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _warehouseContext.Departments
                .Include(d=>d.Products).ToListAsync();
        }
    }
}