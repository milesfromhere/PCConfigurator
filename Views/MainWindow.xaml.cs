using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PCConfigurator.Data;
using PCConfigurator.ViewModels;

namespace PCConfigurator
{
    public partial class MainWindow : Window
    {
        public MainWindow(User user) : this()
        {
            DataContext = new MainViewModel(user);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                if (e.Delta > 0)
                    scrollViewer.LineUp();
                else
                    scrollViewer.LineDown();
                e.Handled = true;
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander currentExpander)
            {
                // Закрываем все остальные expander
                var expanderNames = new[] { "CPUExpander", "GPUExpander", "RAMExpander", "MBExpander",
                                      "PSUExpander", "StorageExpander", "CoolerExpander", "CaseExpander" };
                foreach (var name in expanderNames)
                {
                    if (this.FindName(name) is Expander expander && expander != currentExpander)
                    {
                        expander.IsExpanded = false;
                    }
                }

                // Ищем ListBox внутри открытого expander
                ListBox listBox = currentExpander.Content as ListBox;
                if (listBox == null)
                {
                    listBox = FindChild<ListBox>(currentExpander);
                }
                if (listBox != null && listBox.SelectedItem != null)
                {
                    // Отложенный вызов для прокрутки списка (на случай, если элементы ещё не отрисованы)
                    listBox.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        listBox.ScrollIntoView(listBox.SelectedItem);
                    }));
                }

                // Принудительно обновляем выбранный компонент, чтобы его информация отобразилась
                if (DataContext is MainViewModel vm)
                {
                    vm.RefreshSelectedComponent();
                }
            }
        }

        /// <summary>
        /// Рекурсивно ищет в визуальном дереве дочерний элемент указанного типа.
        /// </summary>
        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                return null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T properlyTyped)
                {
                    return properlyTyped;
                }
                else
                {
                    var childOfChild = FindChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void ThemeSwitchButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}