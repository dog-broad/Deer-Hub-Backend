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
                    int departmentId = InputHelper.PromptInt("Department ID");
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
                    string newFullName = InputHelper.Prompt("New Full Name (optional)", false);
                    string deptInput = InputHelper.Prompt("New Department ID (optional)", false);
                    string dateInput = InputHelper.Prompt("New Date of Joining (optional)", false);
                    string newPhoneNumber = InputHelper.Prompt("New Phone Number (optional)", false);

                    int? newDeptId = null;
                    if (int.TryParse(deptInput, out int parsedDeptId))
                        newDeptId = parsedDeptId;

                    DateTime? newDateOfJoining = null;
                    if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                        newDateOfJoining = parsedDate;

                    string updateResult = service.UpdateEmployee(updateId,
                        string.IsNullOrWhiteSpace(newFullName) ? null : newFullName,
                        newDeptId,
                        newDateOfJoining,
                        string.IsNullOrWhiteSpace(newPhoneNumber) ? null : newPhoneNumber);
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
