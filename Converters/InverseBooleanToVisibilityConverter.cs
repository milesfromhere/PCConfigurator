﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    /// <summary>
    /// Инвертирует булевое значение: true → Collapsed, false → Visible.
    /// </summary>
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
