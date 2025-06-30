using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Validators;

namespace Deer_Hub_Backend.Services
{
    public class LeaveService
    {
        private readonly LeaveRepository _repository;

        public LeaveService(LeaveRepository repository)
        {
            _repository = repository;
        }

        public string ApplyForLeave(LeaveRequest newLeave, Employee emp)
        {
            // Rule 1: Validate date range
            if (!LeaveValidator.IsDateRangeValid(newLeave.StartDate, newLeave.EndDate))
                return "Invalid date range.";

            // Rule 2: Check eligibility
            if (!LeaveValidator.IsEligibleForLeave(emp, newLeave.StartDate))
                return "You are not eligible to apply for leave yet.";

            // Rule 3: Check overlapping leaves
            var existingLeaves = _repository.GetLeavesByEmployee(newLeave.EmployeeID);
            if (LeaveValidator.IsOverlapping(newLeave.StartDate, newLeave.EndDate, existingLeaves))
                return "Leave request overlaps with an existing leave.";

            // If valid, insert
            _repository.SubmitLeaveRequest(newLeave);
            return "Leave request submitted successfully.";
        }
    }
}