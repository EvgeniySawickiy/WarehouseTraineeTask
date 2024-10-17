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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> RegisterUser(UserRequestDTO userRequestDTO)
        {
            var user = _mapper.Map<User>(userRequestDTO);
            user = await _userService.RegisterAsync(user);

            var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return CreatedAtAction(nameof(GetUser), new { id = userResponseDTO.Id }, userResponseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(user.Id);
            return NoContent();
        }
    }
}