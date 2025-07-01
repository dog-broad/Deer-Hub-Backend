using System;
using System.Collections.Generic;
using Deer_Hub_Backend.Models;
using DAL;

namespace Deer_Hub_Backend.Services
{
    public class LeaveStatusService
    {
        private readonly LeaveStatusRepository _repository;

        public LeaveStatusService(LeaveStatusRepository repository)
        {
            _repository = repository;
        }

        public string CreateStatus(string statusName)
        {
            if (string.IsNullOrWhiteSpace(statusName))
                return "Status name cannot be empty.";

            var status = new LeaveStatus { StatusName = statusName };
            bool created = _repository.InsertLeaveStatus(status);
            return created ? "Leave status created successfully." : "Failed to create leave status.";
        }

        public List<LeaveStatus> GetAllStatuses()
        {
            return _repository.GetAllStatuses();
        }

        public string UpdateStatus(int id, string statusName)
        {
            if (string.IsNullOrWhiteSpace(statusName))
                return "Status name cannot be empty.";

            bool updated = _repository.UpdateStatus(id, statusName);
            return updated ? "Leave status updated successfully." : "Failed to update leave status.";
        }

        public string DeleteStatus(int id)
        {
            bool deleted = _repository.DeleteStatus(id);
            return deleted ? "Leave status deleted successfully." : "Failed to delete leave status.";
        }
    }
}
