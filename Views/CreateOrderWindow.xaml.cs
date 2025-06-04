using System.Windows;
using PCConfigurator.ViewModels;
using PCConfigurator.Data;
using System.Collections.Generic;

namespace PCConfigurator.Views
{
    public partial class CreateOrderWindow : Window
    {
        public CreateOrderWindow(User currentUser, IEnumerable<ComponentEntity> components)
        {
            InitializeComponent();
            DataContext = new CreateOrderViewModel(currentUser, components);
        }
    }
} 