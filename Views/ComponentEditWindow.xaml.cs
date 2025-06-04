using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PCConfigurator.Data;
using Microsoft.EntityFrameworkCore;

namespace PCConfigurator
{
    public partial class ComponentEditWindow : Window
    {

        // Используем модель для редактирования, аналогичную компоненту, без служебных ID
        public ComponentEditModel Component { get; set; }
        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
        {
            "Процессор", "Видеокарта", "Оперативная память",
            "Материнская плата", "Блок питания", "Накопитель",
            "Кулер", "Корпус"
        };

        private string _specificationsText;
        public string SpecificationsText
        {
            get => _specificationsText ?? string.Join("\n", Component.Specifications);
            set
            {
                _specificationsText = value;
                Component.Specifications = value.Split('\n')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ComponentEditWindow()
        {
            InitializeComponent();
            try
            {
                Component = new ComponentEditModel();
                DataContext = this;
                SaveCommand = new RelayCommand(Save);
                CancelCommand = new RelayCommand(Cancel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Конструктор для редактирования существующего компонента
        public ComponentEditWindow(ComponentEntity component) : this()
        {
            if (component != null)
            {
                Component = new ComponentEditModel
                {
                    Name = component.Name,
                    Price = component.Price,
                    ImagePath = component.ImagePath,
                    Category = component.Category != null ? component.Category.CategoryName : "",
                    Specifications = new List<string>(component.SpecificationsText)
                };
                _specificationsText = string.Join("\n", component.SpecificationsText);
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Component.Name))
            {
                MessageBox.Show("Введите название компонента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Cancel()
        {
            DialogResult = false;
            Close();
        }
    }

    // Простая модель для редактирования компонента
    public class ComponentEditModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public List<string> Specifications { get; set; } = new List<string>();
        public string Category { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhone { get; set; }
        public string OrderDetails { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Stock { get; set; }
    }
}
