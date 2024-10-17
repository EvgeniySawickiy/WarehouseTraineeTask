using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Application.DTOs.ResponseDTO
{
    public class DepartmentResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
