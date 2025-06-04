using System;
using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; } 
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public bool? IsApproved { get; set; }
        public int ComponentId { get; set; }
        public virtual User User { get; set; }
        public virtual ComponentEntity Component { get; set; }
    }
}
