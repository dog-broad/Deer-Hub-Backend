using System;
using System.Collections.Generic;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public string CreateUser(string username, string email, string passwordHash, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(role))
                return "All user fields are required.";

            var user = new User
            {
                Username = username.Trim(),
                Email = email.Trim(),
                PasswordHash = passwordHash,
                Role = role.Trim(),
                IsActive = true
            };

            bool created = _repository.InsertUser(user);
            return created ? "User created successfully." : "Failed to create user.";
        }

        public List<User> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public string UpdateUser(int userId, string? username = null, string? email = null, string? passwordHash = null, string? role = null, bool? isActive = null)
        {
            if (username == null && email == null && passwordHash == null && role == null && isActive == null)
                return "Nothing to update.";

            bool updated = _repository.UpdateUser(userId, username?.Trim(), email?.Trim(), passwordHash, role?.Trim(), isActive);
            return updated ? "User updated successfully." : "Failed to update user.";
        }

        public string DeleteUser(int userId)
        {
            bool deleted = _repository.DeleteUser(userId);
            return deleted ? "User deleted successfully." : "Failed to delete user.";
        }
    }
}
