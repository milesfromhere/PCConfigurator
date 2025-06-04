using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class BoolToAuthCommandConverter : IValueConverter
    {
        // Если режим входа – возвращаем LoginCommand, иначе RegisterCommand.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Application.Current.Windows.Count > 0 &&
                Application.Current.Windows[0].DataContext is PCConfigurator.AuthViewModel vm)
            {
                bool isLoginMode = value is bool b && b;
                return isLoginMode ? vm.LoginCommand : vm.RegisterCommand;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
