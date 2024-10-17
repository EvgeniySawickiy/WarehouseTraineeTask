﻿

namespace WarehouseTraineeTask.Domain.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        public string Role { get; set; } 
        public DateTime CreatedAt { get; set; }

        public string AuthProvider { get; set; }
        public string ProviderUserId { get; set; }
    }
}