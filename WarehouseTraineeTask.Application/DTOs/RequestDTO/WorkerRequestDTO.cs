

namespace WarehouseTraineeTask.Application.DTOs.RequestDTO
{
    public class WorkerRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public List<Guid> DepartmentIds { get; set; }
    }
}