// File: Program.cs
using DAL;
using Deer_Hub_Backend.DAL;

class Program
{
    static void Main(string[] args)
    {
        // Instantiate Repositories
        var empRepo = new EmployeeRepository();
        var deptRepo = new DepartmentRepository();
        var typeRepo = new LeaveTypeRepository();
        var statusRepo = new LeaveStatusRepository();
        var annRepo = new AnnouncementRepository();
        var leaveRepo = new LeaveRepository();

        Console.WriteLine("===== DEERHub Data Overview =====");

        Console.WriteLine("\n📁 Departments:");
        foreach (var d in deptRepo.GetAllDepartments())
            Console.WriteLine($" - {d.DepartmentID}: {d.Name} ({d.Description})");

        Console.WriteLine("\n📋 Leave Types:");
        foreach (var t in typeRepo.GetAllLeaveTypes())
            Console.WriteLine($" - {t.LeaveTypeID}: {t.Name} ({t.Description})");

        Console.WriteLine("\n🔄 Leave Statuses:");
        foreach (var s in statusRepo.GetAllStatuses())
            Console.WriteLine($" - {s.StatusID}: {s.StatusName}");

        Console.WriteLine("\n👥 Employees:");
        foreach (var e in empRepo.GetAllEmployees())
            Console.WriteLine($" - {e.EmployeeID}: {e.FullName} | Phone: {e.PhoneNumber}");

        Console.WriteLine("\n📢 Announcements:");
        foreach (var a in annRepo.GetAllAnnouncements())
            Console.WriteLine($" - {a.AnnouncementID}: \"{a.Title}\" by UserID {a.PostedBy} on {a.PostedAt}");

        Console.WriteLine("\n📅 Leave Requests by Type (TypeID = 1):");
        foreach (var l in leaveRepo.GetLeavesByType(1))
            Console.WriteLine($" - LeaveID {l.LeaveID}: {l.Reason} (From {l.StartDate.ToShortDateString()} to {l.EndDate.ToShortDateString()})");

        Console.WriteLine("\n✅ Leave Requests by Status (StatusID = 2):");
        foreach (var l in leaveRepo.GetLeavesByStatus(2))
            Console.WriteLine($" - LeaveID {l.LeaveID}: {l.Reason}, Status: {l.StatusID}");

        Console.WriteLine("\n✅ Program completed successfully.");
        Console.ReadLine();
    }
}
