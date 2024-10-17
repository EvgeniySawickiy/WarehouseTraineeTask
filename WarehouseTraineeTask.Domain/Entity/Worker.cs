
using System.Text.Json.Serialization;

namespace WarehouseTraineeTask.Domain.Entity
{
    public class Worker
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }

        [JsonIgnore]
        public ICollection<WorkerDepartment> WorkerDepartments { get; set; }
    }
}