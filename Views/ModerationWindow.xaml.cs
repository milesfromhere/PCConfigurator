using System.Windows;
using PCConfigurator.ViewModels;

namespace PCConfigurator.Views
{
    public partial class ModerationWindow : Window
    {
        public ModerationWindow()
        {
            InitializeComponent();
            DataContext = new ModerationViewModel();
        }
    }
} 