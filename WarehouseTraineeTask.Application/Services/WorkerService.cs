using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public WorkerService(IWorkerRepository workerRepository, IDepartmentRepository departmentRepository)
        {
            _workerRepository = workerRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await _workerRepository.GetAllAsync();
        }

        public async Task<Worker> GetByIdAsync(Guid id)
        {
            return await _workerRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Worker worker, List<Guid>? departmentId)
        {
            var newWorker = new Worker
            {
                Id = Guid.NewGuid(),
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Email = worker.Email,
                Phone = worker.Phone,
                Position = worker.Position,
                HireDate = worker.HireDate,
            };

            if (departmentId != null && departmentId.Any())
            {
                worker.WorkerDepartments = new List<WorkerDepartment>();

                foreach (var Id in departmentId)
                {
                    var department = await _departmentRepository.GetByIdAsync(Id);
                    if (department != null)
                    {
                        worker.WorkerDepartments.Add(new WorkerDepartment
                        {
                            WorkerId = newWorker.Id,
                            DepartmentId = department.Id
                        });
                    }
                }
            }

            await _workerRepository.AddAsync(worker);
        }

        public async Task UpdateAsync(Guid id, Worker worker)
        {
            worker.Id = id;
            _workerRepository.Update(worker);
        }

        public async Task DeleteAsync(Guid id)
        {
           var worker = _workerRepository.GetByIdAsync(id).Result;
            if (worker != null)
            {
                _workerRepository.Delete(worker);
            }
        }
    }
}