using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            var vm = new AuthViewModel();
            // Устанавливаем действие закрытия окна для VM
            vm.CloseAction = new System.Action(this.Close);
            DataContext = vm;
        }
    }
}
