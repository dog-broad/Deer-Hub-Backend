using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deer_Hub_Backend.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PostedBy { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsVisible { get; set; }
    }
}
