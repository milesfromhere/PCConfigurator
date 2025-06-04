using System;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class BoolToSwitchButtonConverter : IValueConverter
    {
        // Если режим входа – возвращаем "Регистрация", иначе "Вход"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isLoginMode = value is bool b && b;
            return isLoginMode ? "Регистрация" : "Вход";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
