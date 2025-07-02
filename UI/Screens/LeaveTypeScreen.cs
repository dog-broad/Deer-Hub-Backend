using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class LeaveTypeScreen
    {
        public static void Show()
        {
            var service = new LeaveTypeService(new LeaveTypeRepository());

            var options = new List<string>
            {
                "List all leave types",
                "Create new leave type",
                "Update leave type",
                "Delete leave type",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Leave Type Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var allTypes = service.GetAllLeaveTypes();
                    AsciiTableHelper.PrintTable(allTypes);
                    break;

                case 1:
                    string name = InputHelper.Prompt("Leave type name");
                    string description = InputHelper.Prompt("Leave type description (optional)", false);
                    string createResult = service.CreateLeaveType(name, description);
                    Console.WriteLine(createResult);
                    break;

                case 2:
                    var leaveTypeService = new LeaveTypeService(new LeaveTypeRepository());
                    int updateId = InputHelper.PromptLeaveTypeId(leaveTypeService, "Leave type ID to update");
                    var repo = new LeaveTypeRepository();
                    var existing = repo.GetLeaveTypeById(updateId);
                    if (existing == null)
                    {
                        Console.WriteLine("Leave type not found.");
                        break;
                    }
                    string newName = InputHelper.Prompt($"New name (current: {existing.Name})", false);
                    string newDesc = InputHelper.Prompt($"New description (current: {existing.Description})", false);
                    existing.Name = string.IsNullOrWhiteSpace(newName) ? existing.Name : newName;
                    existing.Description = string.IsNullOrWhiteSpace(newDesc) ? existing.Description : newDesc;
                    string updateResult = leaveTypeService.UpdateLeaveType(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    var leaveTypeServiceDel = new LeaveTypeService(new LeaveTypeRepository());
                    int deleteId = InputHelper.PromptLeaveTypeId(leaveTypeServiceDel, "Leave type ID to delete");
                    string deleteResult = leaveTypeServiceDel.DeleteLeaveType(deleteId);
                    Console.WriteLine(deleteResult);
                    break;

                default:
                    return;
            }
        }
    }
}
