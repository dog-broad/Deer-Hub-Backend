using System;
using System.Collections.Generic;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public string CreateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FullName))
                return "Full name is required.";

            if (employee.DepartmentID <= 0)
                return "Invalid department ID.";

            if (employee.DateOfJoining > DateTime.Now)
                return "Date of joining cannot be in the future.";

            bool success = _repository.InsertEmployee(employee);
            return success ? "Employee created successfully." : "Failed to create employee.";
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAllEmployees();
        }

        public string UpdateEmployee(int employeeId, string? fullName = null, int? departmentId = null, DateTime? dateOfJoining = null)
        {
            if (fullName == null && departmentId == null && dateOfJoining == null)
                return "Nothing to update.";

            bool success = _repository.UpdateEmployee(employeeId, fullName, departmentId, dateOfJoining);
            return success ? "Employee updated successfully." : "Failed to update employee.";
        }

        public string DeleteEmployee(int employeeId)
        {
            bool success = _repository.DeleteEmployee(employeeId);
            return success ? "Employee deleted successfully." : "Failed to delete employee.";
        }
    }
}
