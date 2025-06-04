using PCConfigurator.Data;
using System.Collections.Generic;

namespace PCConfigurator.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }

        // Коллекция сборок пользователя
        public List<Build> Builds { get; set; } = new List<Build>();

        // Коллекция избранных компонентов пользователя
        public List<ComponentEntity> Favorites { get; set; } = new List<ComponentEntity>();
    }
}
