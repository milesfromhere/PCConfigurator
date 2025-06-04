using System.Windows;
using PCConfigurator.Data;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class ReviewsWindow : Window
    {
        public ReviewsWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = new ReviewsViewModel(currentUser);
        }

        public ReviewsWindow(User currentUser, int componentId)
        {
            InitializeComponent();
            DataContext = new ReviewsViewModel(currentUser, componentId);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
