using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class OrderHistoryWindow : Window
    {
        public OrderHistoryWindow(int userId)
        {
            InitializeComponent();
            DataContext = new OrderHistoryViewModel(userId);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
