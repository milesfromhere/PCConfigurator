using System;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class ThemeIconConverter : IValueConverter
    {
        // Статическое свойство для доступа через x:Static в XAML
        public static ThemeIconConverter Instance { get; } = new ThemeIconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDarkTheme = value is bool b && b;
            return isDarkTheme ? "🌙" : "☀️";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
