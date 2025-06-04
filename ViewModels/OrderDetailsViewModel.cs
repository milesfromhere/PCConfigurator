using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PCConfigurator.Data;
using Microsoft.EntityFrameworkCore;

namespace PCConfigurator.ViewModels
{
    public class OrderDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhone { get; set; }
        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; } = new ObservableCollection<OrderItemViewModel>();

        public OrderDetailsViewModel(OrderViewModel order)
        {
            OrderId = order.OrderId;
            OrderDate = order.OrderDate;
            TotalAmount = order.TotalAmount;
            Status = order.Status;
            DeliveryAddress = order.DeliveryAddress;
            ContactPhone = order.ContactPhone;

            LoadOrderItems();
        }

        private void LoadOrderItems()
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var items = context.OrderItems
                        .Include(oi => oi.Component)
                        .Where(oi => oi.OrderId == OrderId)
                        .Select(oi => new OrderItemViewModel
                        {
                            ComponentId = oi.ComponentID,
                            ComponentName = oi.Component.Name,
                            Price = oi.Price,
                            Quantity = oi.Quantity
                        })
                        .ToList();

                    foreach (var item in items)
                        OrderItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки деталей заказа: {ex.Message}", 
                    "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }

    public class OrderItemViewModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
} 