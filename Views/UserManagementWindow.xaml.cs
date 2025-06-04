using System.Windows;
using PCConfigurator.Data;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class UserManagementWindow : Window
    {
            public UserManagementWindow(User currentUser)
            {
                InitializeComponent();
                DataContext = new UserManagementViewModel(currentUser);
            }

    }
}
