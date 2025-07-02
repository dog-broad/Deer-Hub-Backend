using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.UI.Helpers;
using Deer_Hub_Backend.Constants;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class LeaveScreen
    {
        public static void Show()
        {
            var service = new LeaveService(new LeaveRepository());

            var options = new List<string>
            {
                "List all leave requests",
                "List leaves by employee",
                "List leaves by status",
                "Apply for leave",
                "Approve leave",
                "Reject leave",
                "Cancel leave",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Leave Request Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var allLeaves = service.GetAllLeaveRequests();
                    AsciiTableHelper.PrintTable(allLeaves);
                    break;

                case 1:
                    int empId = InputHelper.PromptInt("Employee ID");
                    var empLeaves = service.GetLeavesForEmployee(empId);
                    AsciiTableHelper.PrintTable(empLeaves);
                    break;

                case 2:
                    Console.WriteLine("Leave Status IDs:");
                    Console.WriteLine($"  {LeaveStatusIds.Pending} - Pending");
                    Console.WriteLine($"  {LeaveStatusIds.Approved} - Approved");
                    Console.WriteLine($"  {LeaveStatusIds.Rejected} - Rejected");
                    Console.WriteLine($"  {LeaveStatusIds.Cancelled} - Cancelled");
                    int statusId = InputHelper.PromptInt("Enter status ID");
                    var leavesByStatus = service.GetLeavesByStatus(statusId);
                    AsciiTableHelper.PrintTable(leavesByStatus);
                    break;

                case 3:
                    int employeeId = InputHelper.PromptInt("Employee ID");
                    int leaveTypeId = InputHelper.PromptInt("Leave Type ID");
                    DateTime startDate = InputHelper.PromptDate("Start Date");
                    DateTime endDate = InputHelper.PromptDate("End Date");
                    string reason = InputHelper.Prompt("Reason");
                    var newLeave = new LeaveRequest
                    {
                        EmployeeID = employeeId,
                        LeaveTypeID = leaveTypeId,
                        StatusID = 1,
                        StartDate = startDate,
                        EndDate = endDate,
                        Reason = reason
                    };
                    var emp = new DAL.EmployeeRepository().GetEmployeeById(employeeId);
                    if (emp == null)
                    {
                        Console.WriteLine("Employee not found.");
                        break;
                    }
                    string applyResult = service.ApplyForLeave(newLeave, emp);
                    Console.WriteLine(applyResult);
                    break;

                case 4:
                    int approveId = InputHelper.PromptInt("Leave Request ID to approve");
                    Console.WriteLine(service.ApproveLeave(approveId));
                    break;

                case 5:
                    int rejectId = InputHelper.PromptInt("Leave Request ID to reject");
                    Console.WriteLine(service.RejectLeave(rejectId));
                    break;

                case 6:
                    int cancelId = InputHelper.PromptInt("Leave Request ID to cancel");
                    Console.WriteLine(service.CancelLeave(cancelId));
                    break;

                default:
                    return;
            }
        }
    }
}
