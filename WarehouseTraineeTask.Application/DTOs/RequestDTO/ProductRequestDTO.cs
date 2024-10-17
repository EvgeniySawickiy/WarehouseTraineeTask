

namespace WarehouseTraineeTask.Application.DTOs.RequestDTO
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid DepartmentId { get; set; }
    }
}