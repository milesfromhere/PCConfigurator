using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class ActiveOrdersWindow : Window
    {
        public ActiveOrdersWindow()
        {
            InitializeComponent();
            DataContext = new ActiveOrdersViewModel();
        }
    }
}
