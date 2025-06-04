using System.Collections.Generic;

namespace PCConfigurator.Data
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<ComponentEntity> Components { get; set; } = new List<ComponentEntity>();
    }
}
