using System;
using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Data
{
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
