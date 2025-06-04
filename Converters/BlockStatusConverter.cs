using System;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class BlockStatusConverter : IValueConverter
    {
        // Если пользователь заблокирован, возвращаем "Разблокировать", иначе "Блокировать"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isBlocked = value is bool b && b;
            return isBlocked ? "Разблокировать" : "Блокировать";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
