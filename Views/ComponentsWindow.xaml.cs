using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PCConfigurator.Views;
using PCConfigurator.Data;

namespace PCConfigurator
{
    public partial class ComponentsWindow : Window
    {
        public ComponentsWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = new ComponentsViewModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                (mainWindow.DataContext as MainViewModel)?.RefreshData();
            }
        }
        private void OpenReviews_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ComponentsViewModel vm && vm.SelectedComponent != null)
            {
                var user = Application.Current.Properties["CurrentUser"] as User;
                var reviewsWindow = new ReviewsWindow(user, vm.SelectedComponent.ComponentID);
                reviewsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Сначала выберите компонент.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}