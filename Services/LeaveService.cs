using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Validators;
using Deer_Hub_Backend.Constants;

public class LeaveService
{
    private readonly LeaveRepository _repository;

    public LeaveService(LeaveRepository repository)
    {
        _repository = repository;
    }

    public string ApplyForLeave(LeaveRequest newLeave, Employee emp)
    {
        if (!LeaveValidator.IsDateRangeValid(newLeave.StartDate, newLeave.EndDate))
            return "Invalid date range.";

        if (!LeaveValidator.IsEligibleForLeave(emp, newLeave.StartDate))
            return "You are not eligible to apply for leave yet.";

        var existingLeaves = _repository.GetLeavesByEmployee(newLeave.EmployeeID);
        if (LeaveValidator.IsOverlapping(newLeave.StartDate, newLeave.EndDate, existingLeaves))
            return "Leave request overlaps with an existing leave.";

        if (!LeaveValidator.IsFutureDate(newLeave.StartDate) || !LeaveValidator.IsFutureDate(newLeave.EndDate))
            return "Leave dates cannot be in the past.";

        if (!LeaveValidator.IsReasonValid(newLeave.Reason))
            return "Leave reason must be at least 5 characters long.";

        if (!LeaveValidator.IsWithinMaxLeaveDays(newLeave.StartDate, newLeave.EndDate, 30))
            return "Leave duration cannot exceed 30 days.";

        newLeave.StatusID = LeaveStatusIds.Pending;

        bool submit = _repository.InsertLeaveRequest(newLeave);
        return submit ? "Leave request submitted successfully." : "Failed to submit leave request.";
    }

    public string ApproveLeave(int leaveId)
    {
        bool updated = _repository.UpdateLeaveStatus(leaveId, LeaveStatusIds.Approved);
        return updated ? "Leave approved successfully." : "Failed to approve leave.";
    }

    public string RejectLeave(int leaveId)
    {
        bool updated = _repository.UpdateLeaveStatus(leaveId, LeaveStatusIds.Rejected);
        return updated ? "Leave rejected." : "Failed to reject leave.";
    }

    public string CancelLeave(int leaveId)
    {
        bool updated = _repository.UpdateLeaveStatus(leaveId, LeaveStatusIds.Cancelled);
        return updated ? "Leave cancelled successfully." : "Failed to cancel leave.";
    }

    public List<LeaveRequest> GetLeavesForEmployee(int employeeId)
    {
        return _repository.GetLeavesByEmployee(employeeId);
    }

    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _repository.GetAllLeaveRequests();
    }

    public List<LeaveRequest> GetLeavesByStatus(int statusId)
    {
        return _repository.GetLeavesByStatus(statusId);
    }
}
