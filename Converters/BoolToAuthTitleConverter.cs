using System;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class BoolToAuthTitleConverter : IValueConverter
    {
        // Если IsLoginMode==true, возвращаем "Вход", иначе "Регистрация"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isLoginMode = value is bool b && b;
            return isLoginMode ? "Вход" : "Регистрация";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
