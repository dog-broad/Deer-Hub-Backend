using DAL;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class LeaveStatusScreen
    {
        public static void Show()
        {
            var service = new LeaveStatusService(new LeaveStatusRepository());

            var options = new List<string>
            {
                "List all leave statuses",
                "Create new leave status",
                "Update leave status",
                "Delete leave status",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Leave Status Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var allStatuses = service.GetAllStatuses();
                    AsciiTableHelper.PrintTable(allStatuses);
                    break;

                case 1:
                    string statusName = InputHelper.Prompt("Leave status name");
                    string createResult = service.CreateStatus(statusName);
                    Console.WriteLine(createResult);
                    break;

                case 2:
                    var leaveStatusService = new LeaveStatusService(new LeaveStatusRepository());
                    int updateId = InputHelper.PromptLeaveStatusId(leaveStatusService, "Leave status ID to update");
                    var repo = new LeaveStatusRepository();
                    var existing = repo.GetLeaveStatusById(updateId);
                    if (existing == null)
                    {
                        Console.WriteLine("Leave status not found.");
                        break;
                    }
                    string newName = InputHelper.Prompt($"New status name (current: {existing.StatusName})");
                    existing.StatusName = string.IsNullOrWhiteSpace(newName) ? existing.StatusName : newName;
                    string updateResult = leaveStatusService.UpdateStatus(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    var leaveStatusServiceDel = new LeaveStatusService(new LeaveStatusRepository());
                    int deleteId = InputHelper.PromptLeaveStatusId(leaveStatusServiceDel, "Leave status ID to delete");
                    string deleteResult = leaveStatusServiceDel.DeleteStatus(deleteId);
                    Console.WriteLine(deleteResult);
                    break;

                default:
                    return;
            }
        }
    }
}
