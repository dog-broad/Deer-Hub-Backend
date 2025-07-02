namespace Deer_Hub_Backend.UI.Helpers
{
    public static class InputHelper
    {
        public static string Prompt(string label, bool required = true)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && !required)
                    return null;
                if (!required || !string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("This field is required.");
            }
        }

        public static int PromptInt(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;

                Console.WriteLine("Invalid number.");
            }
        }

        public static bool PromptBool(string label)
        {
            while (true)
            {
                Console.Write($"{label} (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "y" || input == "yes")
                    return true;
                if (input == "n" || input == "no")
                    return false;
                Console.WriteLine("Please enter 'y' or 'n'.");
            }
        }

        public static DateTime PromptDate(string label)
        {
            while (true)
            {
                Console.Write($"{label} (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                    return result;

                Console.WriteLine("Invalid date.");
            }
        }

        // Prompt for Department ID with options and validation
        public static int PromptDepartmentId(Services.DepartmentService departmentService, string label)
        {
            var departments = departmentService.GetAllDepartments();
            Console.WriteLine("Available Departments:");
            foreach (var dept in departments)
                Console.WriteLine($"  {dept.DepartmentID}: {dept.Name}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (departments.Any(d => d.DepartmentID == id))
                    break;
                Console.WriteLine("Invalid Department ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for LeaveType ID with options and validation
        public static int PromptLeaveTypeId(Services.LeaveTypeService leaveTypeService, string label)
        {
            var types = leaveTypeService.GetAllLeaveTypes();
            Console.WriteLine("Available Leave Types:");
            foreach (var type in types)
                Console.WriteLine($"  {type.LeaveTypeID}: {type.Name}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (types.Any(t => t.LeaveTypeID == id))
                    break;
                Console.WriteLine("Invalid Leave Type ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for LeaveStatus ID with options and validation
        public static int PromptLeaveStatusId(Services.LeaveStatusService leaveStatusService, string label)
        {
            var statuses = leaveStatusService.GetAllStatuses();
            Console.WriteLine("Available Leave Statuses:");
            foreach (var status in statuses)
                Console.WriteLine($"  {status.StatusID}: {status.StatusName}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (statuses.Any(s => s.StatusID == id))
                    break;
                Console.WriteLine("Invalid Leave Status ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for Role with options and validation
        public static string PromptRole(string label)
        {
            var roles = new[] { "Employee", "Manager", "Admin" };
            Console.WriteLine("Available Roles: " + string.Join(", ", roles));
            string role;
            while (true)
            {
                role = Prompt(label);
                if (roles.Contains(role, StringComparer.OrdinalIgnoreCase))
                    break;
                Console.WriteLine("Invalid role. Please select from the list above.");
            }
            return role;
        }

        // Prompt for User ID with options and validation
        public static int PromptUserId(Services.UserService userService, string label)
        {
            var users = userService.GetAllUsers();
            Console.WriteLine("Available Users:");
            foreach (var user in users)
                Console.WriteLine($"  {user.UserID}: {user.Username} ({user.Email})");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (users.Any(u => u.UserID == id))
                    break;
                Console.WriteLine("Invalid User ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for Employee ID with options and validation
        public static int PromptEmployeeId(Services.EmployeeService employeeService, string label)
        {
            var employees = employeeService.GetAllEmployees();
            Console.WriteLine("Available Employees:");
            foreach (var emp in employees)
                Console.WriteLine($"  {emp.EmployeeID}: {emp.FullName}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (employees.Any(e => e.EmployeeID == id))
                    break;
                Console.WriteLine("Invalid Employee ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for Leave ID with options and validation
        public static int PromptLeaveId(Services.LeaveService leaveService, string label)
        {
            var leaves = leaveService.GetAllLeaveRequests();
            Console.WriteLine("Available Leave Requests:");
            foreach (var leave in leaves)
                Console.WriteLine($"  {leave.LeaveID}: Employee {leave.EmployeeID}, {leave.StartDate:yyyy-MM-dd} to {leave.EndDate:yyyy-MM-dd}, Status {leave.StatusID}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (leaves.Any(l => l.LeaveID == id))
                    break;
                Console.WriteLine("Invalid Leave ID. Please select from the list above.");
            }
            return id;
        }

        // Prompt for Announcement ID with options and validation
        public static int PromptAnnouncementId(Services.AnnouncementService announcementService, string label)
        {
            var announcements = announcementService.GetVisibleAnnouncements();
            Console.WriteLine("Available Announcements:");
            foreach (var ann in announcements)
                Console.WriteLine($"  {ann.AnnouncementID}: {ann.Title}");
            int id;
            while (true)
            {
                id = PromptInt(label);
                if (announcements.Any(a => a.AnnouncementID == id))
                    break;
                Console.WriteLine("Invalid Announcement ID. Please select from the list above.");
            }
            return id;
        }
    }
}
