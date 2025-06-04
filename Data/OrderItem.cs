using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Data
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ComponentID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ComponentID")]
        public virtual ComponentEntity Component { get; set; }
    }
} 