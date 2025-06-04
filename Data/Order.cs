using System;
using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Data
{
    public enum OrderStatus
    {
        New,
        Processing,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderSummary { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string DeliveryAddress { get; set; }
        public string ContactPhone { get; set; }
        public string OrderDetails { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        // Навигационное свойство
        public virtual User User { get; set; }
    }
}
