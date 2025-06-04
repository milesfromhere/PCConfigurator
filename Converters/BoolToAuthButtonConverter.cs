using System;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class BoolToAuthButtonConverter : IValueConverter
    {
        // Если режим входа – возвращаем "Войти", иначе "Зарегистрироваться"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isLoginMode = value is bool b && b;
            return isLoginMode ? "Войти" : "Зарегистрироваться";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
