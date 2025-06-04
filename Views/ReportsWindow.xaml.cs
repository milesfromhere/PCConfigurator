using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
            DataContext = new ReportsViewModel();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
