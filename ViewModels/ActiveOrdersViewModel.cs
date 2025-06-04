using PCConfigurator.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PCConfigurator.ViewModels
{
    public class ActiveOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<Order> ActiveOrders { get; set; } = new ObservableCollection<Order>();

        public ActiveOrdersViewModel()
        {
            LoadActiveOrders();
        }

        private void LoadActiveOrders()
        {
            ActiveOrders.Clear();
            using (var context = new PCComponentsContext())
            {
                var orders = context.Orders
                    .Where(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                    .OrderBy(o => o.CreatedDate)
                    .ToList();
                foreach (var order in orders)
                    ActiveOrders.Add(order);
            }
        }
    }
}
