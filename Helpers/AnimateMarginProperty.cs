using System.Windows;
using System.Windows.Media.Animation;

namespace PCConfigurator.Helpers
{
    public static class AnimateMarginProperty
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached(
                "IsOpen",
                typeof(bool),
                typeof(AnimateMarginProperty),
                new PropertyMetadata(false, OnIsOpenChanged));

        public static bool GetIsOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                {
                    var storyboard = (Storyboard)element.FindResource("SlideInMenu");
                    storyboard.Begin(element);
                }
                else
                {
                    var storyboard = (Storyboard)element.FindResource("SlideOutMenu");
                    storyboard.Begin(element);
                }
            }
        }
    }
}