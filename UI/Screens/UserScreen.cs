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
                    string role = InputHelper.PromptRole("Role");
                    string result = service.CreateUser(username, email, password, role);
                    Console.WriteLine(result);
                    break;

                case 2:
                    var userService = new UserService(new DAL.UserRepository());
                    int id = InputHelper.PromptUserId(userService, "User ID");
                    var repo = new DAL.UserRepository();
                    var existing = repo.GetUserById(id);
                    if (existing == null)
                    {
                        Console.WriteLine("User not found.");
                        break;
                    }
                    string newUsername = InputHelper.Prompt($"New Username (current: {existing.Username})", false);
                    string newEmail = InputHelper.Prompt($"New Email (current: {existing.Email})", false);
                    string newPassword = InputHelper.Prompt($"New Password Hash (current: {existing.PasswordHash})", false);
                    string newRole = InputHelper.PromptRole($"New Role (current: {existing.Role})");
                    existing.Username = string.IsNullOrWhiteSpace(newUsername) ? existing.Username : newUsername;
                    existing.Email = string.IsNullOrWhiteSpace(newEmail) ? existing.Email : newEmail;
                    existing.PasswordHash = string.IsNullOrWhiteSpace(newPassword) ? existing.PasswordHash : newPassword;
                    existing.Role = string.IsNullOrWhiteSpace(newRole) ? existing.Role : newRole;
                    existing.IsActive = InputHelper.PromptBool($"Is Active (current: {(existing.IsActive ? "Yes" : "No")})");
                    string updateResult = userService.UpdateUser(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    var userServiceDel = new UserService(new DAL.UserRepository());
                    int deleteId = InputHelper.PromptUserId(userServiceDel, "User ID to delete");
                    Console.WriteLine(userServiceDel.DeleteUser(deleteId));
                    break;

                default: return;
            }
        }
    }
}
