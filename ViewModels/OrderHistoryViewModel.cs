using PCConfigurator.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using PCConfigurator.Views;

namespace PCConfigurator.ViewModels
{
    public class OrderHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ICommand ViewOrderDetailsCommand { get; }

        public OrderHistoryViewModel(int userId)
        {
            ViewOrderDetailsCommand = new RelayCommand<Order>(ViewOrderDetails);
            LoadOrders(userId);
        }

        private void LoadOrders(int userId)
        {
            Orders.Clear();
            using (var context = new PCComponentsContext())
            {
                var orders = context.Orders
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.CreatedDate)
                    .ToList();
                foreach (var order in orders)
                    Orders.Add(order);
            }
        }

        private void ViewOrderDetails(Order order)
        {
            if (order == null) return;
            var orderVm = new PCConfigurator.ViewModels.OrderViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.CreatedDate,
                TotalAmount = order.TotalPrice,
                Status = order.Status.ToString(),
                DeliveryAddress = order.DeliveryAddress,
                ContactPhone = order.ContactPhone
            };
            var detailsWindow = new OrderDetailsWindow(orderVm);
            detailsWindow.ShowDialog();
        }
    }
}
