using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class UserScreen
    {
        public static void Show()
        {
            var service = new UserService(new DAL.UserRepository());

            var options = new List<string>
            {
                "List all users",
                "Create new user",
                "Update user",
                "Delete user",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("User Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var users = service.GetAllUsers();
                    AsciiTableHelper.PrintTable(users);
                    break;

                case 1:
                    string username = InputHelper.Prompt("Username");
                    string email = InputHelper.Prompt("Email");
                    string password = InputHelper.Prompt("Password Hash");
                    string role = InputHelper.Prompt("Role");
                    string result = service.CreateUser(username, email, password, role);
                    Console.WriteLine(result);
                    break;

                case 2:
                    int id = InputHelper.PromptInt("User ID");
                    string newUsername = InputHelper.Prompt("New Username (optional)", false);
                    string newEmail = InputHelper.Prompt("New Email (optional)", false);
                    string newRole = InputHelper.Prompt("New Role (optional)", false);
                    bool isActive = InputHelper.PromptBool("Is Active");
                    string updateResult = service.UpdateUser(id, newUsername, newEmail, null, newRole, isActive);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    int deleteId = InputHelper.PromptInt("User ID to delete");
                    Console.WriteLine(service.DeleteUser(deleteId));
                    break;

                default: return;
            }
        }
    }
}
