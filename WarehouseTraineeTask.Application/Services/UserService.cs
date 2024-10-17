using Microsoft.AspNetCore.Identity;
using WarehouseTraineeTask.Application.DTOs.RequestDTO;
using WarehouseTraineeTask.Application.DTOs.ResponseDTO;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> RegisterAsync(User user)
        {
            var newUser = new User
            {
                Id= Guid.NewGuid(),
                Username = user.Username,
                Email = user.Email,
                PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash),
                Role = "User", 
                CreatedAt = DateTime.UtcNow,
                AuthProvider = "Default" 
            };
            await _userRepository.AddAsync(newUser);
            return newUser;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user =_userRepository.GetByIdAsync(id).Result;
            _userRepository.Delete(user);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null ? user : null;
        }
    }
}