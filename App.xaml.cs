using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Views;
using PCConfigurator.Data;

namespace PCConfigurator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var context = new Data.PCComponentsContext())
            {
                context.Database.Migrate();
            }

            this.ShutdownMode = ShutdownMode.OnExplicitShutdown; // Это нужно в начале, чтобы приложение не закрылось сразу

            var authWindow = new Views.AuthWindow();
            Application.Current.MainWindow = authWindow;
            authWindow.ShowDialog();

            var authVM = authWindow.DataContext as AuthViewModel;
            if (authVM?.CurrentUser != null)
            {
                var mainWindow = new MainWindow(authVM.CurrentUser);
                Application.Current.MainWindow = mainWindow;
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();

                // Сохраняем текущего пользователя для глобального доступа
                if (mainWindow.DataContext is MainViewModel vm)
                {
                    Current.Properties["CurrentUser"] = vm.CurrentUser;
                }
            }

        }

    }
}