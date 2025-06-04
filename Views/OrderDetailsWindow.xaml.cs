using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class OrderDetailsWindow : Window
    {
        public OrderDetailsWindow(OrderViewModel order)
        {
            InitializeComponent();
            DataContext = new OrderDetailsViewModel(order);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
} 