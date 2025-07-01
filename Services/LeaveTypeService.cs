using System;
using System.Collections.Generic;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.Services
{
    public class LeaveTypeService
    {
        private readonly LeaveTypeRepository _repository;

        public LeaveTypeService(LeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public string CreateLeaveType(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Leave type name cannot be empty.";

            var type = new LeaveType
            {
                Name = name.Trim(),
                Description = description?.Trim()
            };

            bool created = _repository.InsertLeaveType(type);
            return created ? "Leave type created successfully." : "Failed to create leave type.";
        }

        public List<LeaveType> GetAllLeaveTypes()
        {
            return _repository.GetAllLeaveTypes();
        }

        public string UpdateLeaveType(int id, string? name = null, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description))
                return "Nothing to update.";

            bool updated = _repository.UpdateLeaveType(id, name?.Trim(), description?.Trim());
            return updated ? "Leave type updated successfully." : "Failed to update leave type.";
        }

        public string DeleteLeaveType(int id)
        {
            bool deleted = _repository.DeleteLeaveType(id);
            return deleted ? "Leave type deleted successfully." : "Failed to delete leave type.";
        }
    }
}
