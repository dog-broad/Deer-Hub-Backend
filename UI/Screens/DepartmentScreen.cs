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
                    var departmentService = new DepartmentService(new DepartmentRepository());
                    int updateId = InputHelper.PromptDepartmentId(departmentService, "Department ID");
                    var repo = new DepartmentRepository();
                    var existing = repo.GetDepartmentById(updateId);
                    if (existing == null)
                    {
                        Console.WriteLine("Department not found.");
                        break;
                    }
                    string newName = InputHelper.Prompt($"New Name (current: {existing.Name})", false);
                    string newDescription = InputHelper.Prompt($"New Description (current: {existing.Description})", false);
                    existing.Name = string.IsNullOrWhiteSpace(newName) ? existing.Name : newName;
                    existing.Description = string.IsNullOrWhiteSpace(newDescription) ? existing.Description : newDescription;
                    string updateResult = departmentService.UpdateDepartment(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    var departmentServiceDel = new DepartmentService(new DepartmentRepository());
                    int deleteId = InputHelper.PromptDepartmentId(departmentServiceDel, "Department ID to delete");
                    Console.WriteLine(departmentServiceDel.DeleteDepartment(deleteId));
                    break;

                default:
                    return;
            }
        }
    }
}
