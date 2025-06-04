using System.Windows;
using System.Windows.Controls;

namespace PCConfigurator.Controls
{
    public partial class ThemeSwitchButton : UserControl
    {
        public ThemeSwitchButton()
        {
            InitializeComponent();
        }

        // DependencyProperty для связывания с IsDarkTheme
        public static readonly DependencyProperty IsDarkThemeProperty =
            DependencyProperty.Register("IsDarkTheme", typeof(bool), typeof(ThemeSwitchButton),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsDarkTheme
        {
            get => (bool)GetValue(IsDarkThemeProperty);
            set => SetValue(IsDarkThemeProperty, value);
        }
    }
}
