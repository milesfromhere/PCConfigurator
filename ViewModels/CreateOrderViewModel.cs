using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PCConfigurator.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PCConfigurator.ViewModels
{
    public class CreateOrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public User CurrentUser { get; set; }
        public ObservableCollection<CartItemViewModel> CartItems { get; set; } = new ObservableCollection<CartItemViewModel>();

        private string _deliveryAddress;
        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged(nameof(DeliveryAddress));
            }
        }

        private string _contactPhone;
        public string ContactPhone
        {
            get => _contactPhone;
            set
            {
                _contactPhone = value;
                OnPropertyChanged(nameof(ContactPhone));
            }
        }

        public decimal TotalAmount => CartItems.Sum(item => item.TotalPrice);

        public ICommand RemoveItemCommand { get; }
        public ICommand CreateOrderCommand { get; }

        public CreateOrderViewModel(User currentUser, IEnumerable<ComponentEntity> components)
        {
            CurrentUser = currentUser;
            RemoveItemCommand = new RelayCommand<CartItemViewModel>(RemoveItem);
            CreateOrderCommand = new RelayCommand(CreateOrder);
            CartItems.Clear();
            foreach (var comp in components)
            {
                CartItems.Add(new CartItemViewModel
                {
                    ComponentId = comp.ComponentID,
                    ComponentName = comp.Name,
                    Price = comp.Price,
                    Quantity = 1,
                    Stock = comp.Stock
                });
            }
        }

        private void RemoveItem(CartItemViewModel item)
        {
            CartItems.Remove(item);
            OnPropertyChanged(nameof(TotalAmount));
        }

        private void CreateOrder()
        {
            if (string.IsNullOrWhiteSpace(DeliveryAddress) || string.IsNullOrWhiteSpace(ContactPhone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new PCComponentsContext())
                {
                    var order = new Order
                    {
                        UserId = CurrentUser.UserId,
                        OrderSummary = string.Join("\n", CartItems.Select(item => $"{item.ComponentName} - {item.Quantity} шт. x {item.Price:C} = {item.TotalPrice:C}")),
                        TotalPrice = TotalAmount,
                        Status = OrderStatus.New,
                        CreatedDate = DateTime.Now,
                        DeliveryAddress = DeliveryAddress,
                        ContactPhone = ContactPhone,
                        OrderDetails = string.Join("\n", CartItems.Select(item => $"{item.ComponentName} - {item.Quantity} шт. x {item.Price:C} = {item.TotalPrice:C}")),
                        OrderDate = DateTime.Now,
                        TotalAmount = TotalAmount
                    };

                    context.Orders.Add(order);
                    context.SaveChanges(); // Сохраняем, чтобы получить OrderId

                    foreach (var item in CartItems)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = order.OrderId,
                            ComponentID = item.ComponentId,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        context.OrderItems.Add(orderItem);

                        // Обновляем количество на складе
                        var component = context.Components.FirstOrDefault(c => c.ComponentID == item.ComponentId);
                        if (component != null)
                        {
                            component.Stock -= item.Quantity;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Закрываем окно
                    Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class CartItemViewModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
        public int Stock { get; set; }
    }
} 