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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponseDTO>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            var departmentDTOs = _mapper.Map<IEnumerable<DepartmentResponseDTO>>(departments);
            return Ok(departmentDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentResponseDTO>> GetDepartment(Guid id)
        {
            var department = await _departmentService.GetByIdWithProductsAsync(id);
            if (department == null) return NotFound();

            var departmentDTO = _mapper.Map<DepartmentResponseDTO>(department);
            return Ok(departmentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentResponseDTO>> CreateDepartment(DepartmentRequestDTO departmentRequestDTO)
        {
            var department = _mapper.Map<Department>(departmentRequestDTO);
            department =await _departmentService.AddAsync(department);

            var departmentResponseDTO = _mapper.Map<DepartmentResponseDTO>(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = departmentResponseDTO.Id }, departmentResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentRequestDTO departmentRequestDTO)
        {
            if (id == Guid.Empty) return BadRequest();

            var departmentToUpdate = await _departmentService.GetByIdAsync(id);
            if (departmentToUpdate == null) return NotFound();

            _mapper.Map(departmentRequestDTO, departmentToUpdate);
            await _departmentService.UpdateAsync(id,departmentToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound();

            await _departmentService.DeleteAsync(department.Id);
            return NoContent();
        }
    }
}