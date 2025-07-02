using Deer_Hub_Backend.UI.Helpers;
using Deer_Hub_Backend.UI.Screens;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║    DEER HUB BACKEND CLI    ║");
            Console.WriteLine("╚════════════════════════════╝");

            var options = new List<string>
            {
                "User Service",
                "Employee Service",
                "Department Service",
                "Announcement Service",
                "Leave Request Service",
                "Leave Type Service",
                "Leave Status Service",
                "Exit"
            };

            int choice = MenuHelper.ShowMenu("Main Menu", options);
            Console.Clear();

            switch (choice)
            {
                case 0: UserScreen.Show(); break;
                case 1: EmployeeScreen.Show(); break;
                case 2: DepartmentScreen.Show(); break;
                case 3: AnnouncementScreen.Show(); break;
                case 4: LeaveScreen.Show(); break;
                case 5: LeaveTypeScreen.Show(); break;
                case 6: LeaveStatusScreen.Show(); break;
                case 7: return;
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
