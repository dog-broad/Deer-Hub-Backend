using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class EmployeeScreen
    {
        public static void Show()
        {
            var service = new EmployeeService(new EmployeeRepository());
            var department_service = new DepartmentService(new DepartmentRepository());

            var options = new List<string>
            {
                "List all employees",
                "Create new employee",
                "Update employee",
                "Delete employee",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Employee Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var employees = service.GetAllEmployees();
                    AsciiTableHelper.PrintTable(employees);
                    break;

                case 1:
                    string userIdInput = InputHelper.Prompt("User ID (required)");
                    string fullName = InputHelper.Prompt("Full Name");
                    int departmentId = InputHelper.PromptDepartmentId(department_service, "Department ID");
                    DateTime dateOfJoining = InputHelper.PromptDate("Date of Joining");
                    string phoneNumber = InputHelper.Prompt("Phone Number (optional)", false);
                    var newEmployee = new Employee
                    {
                        UserID = int.Parse(userIdInput),
                        FullName = fullName,
                        DepartmentID = departmentId,
                        DateOfJoining = dateOfJoining,
                        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber,
                    };
                    string createResult = service.CreateEmployee(newEmployee);
                    Console.WriteLine(createResult);
                    break;

                case 2:
                    int updateId = InputHelper.PromptInt("Employee ID");
                    var repo = new EmployeeRepository();
                    var existing = repo.GetEmployeeById(updateId);
                    if (existing == null)
                    {
                        Console.WriteLine("Employee not found.");
                        break;
                    }
                    string newFullName = InputHelper.Prompt($"New Full Name (current: {existing.FullName})", false);
                    string deptInput = null;
                    int? parsedDeptId = null;
                    if (department_service.GetAllDepartments().Count > 0)
                    {
                        parsedDeptId = InputHelper.PromptDepartmentId(department_service, $"New Department ID (current: {existing.DepartmentID})");
                    }
                    string dateInput = InputHelper.Prompt($"New Date of Joining (current: {existing.DateOfJoining:yyyy-MM-dd})", false);
                    string newPhoneNumber = InputHelper.Prompt($"New Phone Number (current: {existing.PhoneNumber})", false);
                    if (!string.IsNullOrWhiteSpace(newFullName)) existing.FullName = newFullName;
                    if (parsedDeptId.HasValue) existing.DepartmentID = parsedDeptId.Value;
                    if (DateTime.TryParse(dateInput, out DateTime parsedDate)) existing.DateOfJoining = parsedDate;
                    if (!string.IsNullOrWhiteSpace(newPhoneNumber)) existing.PhoneNumber = newPhoneNumber;
                    existing.IsActive = InputHelper.PromptBool($"Is Active (current: {(existing.IsActive ? "Yes" : "No")})");
                    string updateResult = service.UpdateEmployee(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    int deleteId = InputHelper.PromptInt("Employee ID to delete");
                    Console.WriteLine(service.DeleteEmployee(deleteId));
                    break;

                default:
                    return;
            }
        }
    }
}
