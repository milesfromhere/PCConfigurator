using System.Windows;
using System.Windows.Controls;

namespace PCConfigurator.Helpers
{
    public static class PasswordBoxAssistant
    {
        // Свойство для привязки значения пароля
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string),
                typeof(PasswordBoxAssistant),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPassword, value);
        }

        // Включатель: нужно ли привязывать пароль
        public static readonly DependencyProperty BindPassword =
            DependencyProperty.RegisterAttached("BindPassword", typeof(bool),
                typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        public static bool GetBindPassword(DependencyObject obj)
        {
            return (bool)obj.GetValue(BindPassword);
        }

        public static void SetBindPassword(DependencyObject obj, bool value)
        {
            obj.SetValue(BindPassword, value);
        }

        private static bool _updating = false;

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (_updating)
                return;
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                string newPassword = (string)e.NewValue;
                if (passwordBox.Password != newPassword)
                    passwordBox.Password = newPassword;
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                bool wasBound = (bool)e.OldValue;
                bool needToBind = (bool)e.NewValue;

                if (wasBound)
                {
                    passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                }
                if (needToBind)
                {
                    passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                }
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _updating = true;
                SetBoundPassword(passwordBox, passwordBox.Password);
                _updating = false;
            }
        }
    }
}
