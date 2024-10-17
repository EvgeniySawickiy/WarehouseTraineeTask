
namespace WarehouseTraineeTask.Domain.Entity
{
    public class WorkerDepartment
    {
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
