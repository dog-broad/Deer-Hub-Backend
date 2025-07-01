using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.Services
{
    public class AnnouncementService
    {
        private readonly AnnouncementRepository _repository;

        public AnnouncementService(AnnouncementRepository repository)
        {
            _repository = repository;
        }

        public string CreateAnnouncement(Announcement announcement)
        {
            bool success = _repository.InsertAnnouncement(announcement);
            return success ? "Announcement posted successfully." : "Failed to post announcement.";
        }

        public List<Announcement> GetVisibleAnnouncements()
        {
            return _repository.GetAllAnnouncements();
        }

        public string UpdateAnnouncement(int id, string? title = null, string? description = null)
        {
            bool success = _repository.UpdateAnnouncement(id, title, description);
            return success ? "Announcement updated successfully." : "No changes made or announcement not found.";
        }

        public string DeleteAnnouncement(int id)
        {
            bool success = _repository.DeleteAnnouncement(id);
            return success ? "Announcement deleted." : "Failed to delete announcement.";
        }
    }
}
