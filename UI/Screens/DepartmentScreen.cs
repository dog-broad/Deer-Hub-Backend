using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class DepartmentScreen
    {
        public static void Show()
        {
            var service = new DepartmentService(new DepartmentRepository());

            var options = new List<string>
            {
                "List all departments",
                "Create new department",
                "Update department",
                "Delete department",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Department Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var departments = service.GetAllDepartments();
                    AsciiTableHelper.PrintTable(departments);
                    break;

                case 1:
                    string name = InputHelper.Prompt("Department Name");
                    string description = InputHelper.Prompt("Description (optional)", false);

                    var newDepartment = new Department
                    {
                        Name = name,
                        Description = string.IsNullOrWhiteSpace(description) ? null : description
                    };

                    string createResult = service.CreateDepartment(newDepartment);
                    Console.WriteLine(createResult);
                    break;

                case 2:
                    int updateId = InputHelper.PromptInt("Department ID");
                    string newName = InputHelper.Prompt("New Name (optional)", false);
                    string newDescription = InputHelper.Prompt("New Description (optional)", false);

                    string updateResult = service.UpdateDepartment(updateId,
                        string.IsNullOrWhiteSpace(newName) ? null : newName,
                        string.IsNullOrWhiteSpace(newDescription) ? null : newDescription);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    int deleteId = InputHelper.PromptInt("Department ID to delete");
                    Console.WriteLine(service.DeleteDepartment(deleteId));
                    break;

                default:
                    return;
            }
        }
    }
}
