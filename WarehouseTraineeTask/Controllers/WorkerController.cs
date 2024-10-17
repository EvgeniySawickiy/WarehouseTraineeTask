using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseTraineeTask.Application.DTOs.RequestDTO;
using WarehouseTraineeTask.Application.DTOs.ResponseDTO;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerService workerService, IMapper mapper)
        {
            _workerService = workerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerResponseDTO>>> GetWorkers()
        {
            var workers = await _workerService.GetAllAsync();
            var workerDTOs = _mapper.Map<IEnumerable<WorkerResponseDTO>>(workers);
            return Ok(workerDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerResponseDTO>> GetWorker(Guid id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            var workerDTO = _mapper.Map<WorkerResponseDTO>(worker);
            return Ok(workerDTO);
        }

        [HttpPost]
        public async Task<ActionResult<WorkerResponseDTO>> CreateWorker(WorkerRequestDTO workerRequestDTO)
        {
            var worker = _mapper.Map<Worker>(workerRequestDTO);
            await _workerService.AddAsync(worker, workerRequestDTO.DepartmentIds);

            var workerResponseDTO = _mapper.Map<WorkerResponseDTO>(worker);
            return Ok(workerResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(Guid id, WorkerRequestDTO workerRequestDTO)
        {
            if (id == Guid.Empty) return BadRequest();

            var workerToUpdate = await _workerService.GetByIdAsync(id);
            if (workerToUpdate == null) return NotFound();

            _mapper.Map(workerRequestDTO, workerToUpdate);
            await _workerService.UpdateAsync(id, workerToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(Guid id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            await _workerService.DeleteAsync(worker.Id);
            return NoContent();
        }
    }
}