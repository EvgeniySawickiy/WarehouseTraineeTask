using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Application.DTOs.ResponseDTO
{
    public class WorkerResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<WorkerDepartment> WorkerDepartments { get; set; }
    }
}