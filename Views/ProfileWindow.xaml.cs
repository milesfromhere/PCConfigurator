using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PCConfigurator.ViewModels;
using PCConfigurator.Views;
using System.Windows.Input;

namespace PCConfigurator.Views
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void UploadAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выбор аватара"
            };

            if (dlg.ShowDialog() == true && DataContext is ProfileViewModel vm)
            {
                vm.AvatarPath = dlg.FileName;
                vm.SaveAvatar();
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Обработка через PasswordBoxAssistant, если необходимо дополнительное поведение
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Обработка через PasswordBoxAssistant, если необходимо дополнительное поведение
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProfileViewModel vm)
            {
                var reviewsWindow = new ReviewsWindow(vm.CurrentUser);
                reviewsWindow.ShowDialog();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            foreach (Window win in Application.Current.Windows.Cast<Window>().ToList())
            {
                win.Close();
            }

            var authWindow = new AuthWindow();
            bool? authResult = authWindow.ShowDialog();
            var authVM = authWindow.DataContext as AuthViewModel;
            if (authVM?.CurrentUser != null)
            {
                var mainWindow = new MainWindow(authVM.CurrentUser);
                Application.Current.MainWindow = mainWindow;
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void ShowNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NewPasswordTextBox.Text = NewPasswordBox.Password;
            NewPasswordBox.Visibility = Visibility.Collapsed;
            NewPasswordTextBox.Visibility = Visibility.Visible;
        }

        private void ShowNewPassword_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NewPasswordBox.Visibility = Visibility.Visible;
            NewPasswordTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowNewPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NewPasswordTextBox.Visibility == Visibility.Visible)
            {
                NewPasswordBox.Visibility = Visibility.Visible;
                NewPasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowRepeatPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RepeatPasswordTextBox.Text = RepeatPasswordBox.Password;
            RepeatPasswordBox.Visibility = Visibility.Collapsed;
            RepeatPasswordTextBox.Visibility = Visibility.Visible;
        }

        private void ShowRepeatPassword_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RepeatPasswordBox.Visibility = Visibility.Visible;
            RepeatPasswordTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowRepeatPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            if (RepeatPasswordTextBox.Visibility == Visibility.Visible)
            {
                RepeatPasswordBox.Visibility = Visibility.Visible;
                RepeatPasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
