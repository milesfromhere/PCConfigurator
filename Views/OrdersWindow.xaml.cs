using System.Windows;
using PCConfigurator.ViewModels;
using PCConfigurator.Data;

namespace PCConfigurator.Views
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = new OrdersViewModel(currentUser);
        }
    }
} 