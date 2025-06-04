using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Data
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public int ComponentID { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ComponentID")]
        public virtual ComponentEntity Component { get; set; }
    }
}
