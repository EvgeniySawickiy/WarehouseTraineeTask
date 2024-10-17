
using System.Text.Json.Serialization;

namespace WarehouseTraineeTask.Domain.Entity
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public ICollection<WorkerDepartment> WorkerDepartments { get; set; }
    }
}