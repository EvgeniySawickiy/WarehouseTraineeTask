

namespace WarehouseTraineeTask.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}