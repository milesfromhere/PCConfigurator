using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace PCConfigurator.Helpers
{
    public static class SlideInBehavior
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached(
                "IsOpen",
                typeof(bool),
                typeof(SlideInBehavior),
                new PropertyMetadata(false, OnIsOpenChanged));

        public static bool GetIsOpen(DependencyObject obj) => (bool)obj.GetValue(IsOpenProperty);
        public static void SetIsOpen(DependencyObject obj, bool value) => obj.SetValue(IsOpenProperty, value);

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                {
                    var animation = new ThicknessAnimation
                    {
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    element.BeginAnimation(FrameworkElement.MarginProperty, animation);
                }
                else
                {
                    var animation = new ThicknessAnimation
                    {
                        To = new Thickness(0, 0, -300, 0),
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };
                    element.BeginAnimation(FrameworkElement.MarginProperty, animation);
                }
            }
        }
    }
}