using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;
using System.Collections.Generic;

namespace Deer_Hub_Backend.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _repository;

        public DepartmentService(DepartmentRepository repository)
        {
            _repository = repository;
        }

        public string CreateDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                return "Department name is required.";

            bool success = _repository.InsertDepartment(department);
            return success ? "Department created successfully." : "Failed to create department.";
        }

        public List<Department> GetAllDepartments()
        {
            return _repository.GetAllDepartments();
        }

        public string UpdateDepartment(int departmentId, string? newName, string? newDescription)
        {
            if (string.IsNullOrWhiteSpace(newName) && string.IsNullOrWhiteSpace(newDescription))
                return "Nothing to update.";

            bool success = _repository.UpdateDepartment(departmentId, newName, newDescription);
            return success ? "Department updated successfully." : "Failed to update department.";
        }

        public string DeleteDepartment(int departmentId)
        {
            bool success = _repository.DeleteDepartment(departmentId);
            return success ? "Department deleted successfully." : "Failed to delete department.";
        }
    }
}
