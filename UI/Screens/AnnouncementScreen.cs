using Deer_Hub_Backend.Models;
using Deer_Hub_Backend.Services;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.UI.Helpers;

namespace Deer_Hub_Backend.UI.Screens
{
    public static class AnnouncementScreen
    {
        public static void Show()
        {
            var service = new AnnouncementService(new AnnouncementRepository());

            var options = new List<string>
            {
                "List all announcements",
                "Create new announcement",
                "Update announcement",
                "Delete announcement",
                "Back"
            };

            int choice = MenuHelper.ShowMenu("Announcement Service", options);
            Console.Clear();

            switch (choice)
            {
                case 0:
                    var announcements = service.GetVisibleAnnouncements();
                    AsciiTableHelper.PrintTable(announcements);
                    break;

                case 1:
                    string title = InputHelper.Prompt("Title");
                    string description = InputHelper.Prompt("Description");
                    var newAnnouncement = new Announcement
                    {
                        Title = title,
                        Description = description
                    };
                    string createResult = service.CreateAnnouncement(newAnnouncement);
                    Console.WriteLine(createResult);
                    break;

                case 2:
                    int updateId = InputHelper.PromptInt("Announcement ID");
                    var repo = new AnnouncementRepository();
                    var existing = repo.GetAnnouncementById(updateId);
                    if (existing == null)
                    {
                        Console.WriteLine("Announcement not found.");
                        break;
                    }
                    string newTitle = InputHelper.Prompt($"New Title (current: {existing.Title})", false);
                    string newDescription = InputHelper.Prompt($"New Description (current: {existing.Description})", false);
                    existing.Title = string.IsNullOrWhiteSpace(newTitle) ? existing.Title : newTitle;
                    existing.Description = string.IsNullOrWhiteSpace(newDescription) ? existing.Description : newDescription;
                    existing.IsVisible = InputHelper.PromptBool($"Is Visible (current: {(existing.IsVisible ? "Yes" : "No")})");
                    string updateResult = service.UpdateAnnouncement(existing);
                    Console.WriteLine(updateResult);
                    break;

                case 3:
                    int deleteId = InputHelper.PromptInt("Announcement ID to delete");
                    Console.WriteLine(service.DeleteAnnouncement(deleteId));
                    break;

                default:
                    return;
            }
        }
    }
}
