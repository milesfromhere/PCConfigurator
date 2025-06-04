using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Data
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; } = "example.email.com";

        public bool IsAdmin { get; set; } = false;

        public string Name { get; set; } = "User";           
        public string AvatarPath { get; set; } = "C:\\Users\\nikit\\OneDrive\\Рабочий стол\\PCConfigurator\\Images\\placeholder.png";

        public bool IsBlocked { get; set; } = false;
    }
}