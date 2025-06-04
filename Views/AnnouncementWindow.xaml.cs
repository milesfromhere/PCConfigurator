using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class AnnouncementsWindow : Window
    {
        public AnnouncementsWindow()
        {
            InitializeComponent();
            DataContext = new AnnouncementsViewModel();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AnnouncementsViewModel vm)
            {
                // Обновляем данные, создавая новый экземпляр ViewModel
                DataContext = new AnnouncementsViewModel();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
