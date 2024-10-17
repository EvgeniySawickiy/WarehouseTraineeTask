using WarehouseTraineeTask.Application.DTOs.ResponseDTO;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Application.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task<Department> AddAsync(Department department)
        {
            var newDepartment = new Department()
            {
                Id = Guid.NewGuid(),
                Name = department.Name,
                Location = department.Location,
                Capacity = department.Capacity,
                CreatedDate = DateTime.UtcNow,
            };

            await _departmentRepository.AddAsync(newDepartment);
            return newDepartment;
        }

        public async Task UpdateAsync(Guid id, Department department)
        {
            department.Id = id;
            _departmentRepository.Update(department);
        }

        public async Task DeleteAsync(Guid id)
        {
            var department = _departmentRepository.GetByIdAsync(id).Result;
            _departmentRepository.Delete(department);
        }

        public async Task<Department> GetByIdWithProductsAsync(Guid id)
        {
            var department = await _departmentRepository.GetByIdWithProductsAsync(id);

            if (department == null)
            {
                return null;
            }

            return new Department
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location,
                Capacity = department.Capacity,
                CreatedDate = department.CreatedDate,
                Products = department.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    ExpiryDate = p.ExpiryDate
                }).ToList()
            };
        }
    }
}