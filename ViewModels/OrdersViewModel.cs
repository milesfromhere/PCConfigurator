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
    public class OrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public User CurrentUser { get; set; }
        public ObservableCollection<OrderViewModel> ActiveOrders { get; set; } = new ObservableCollection<OrderViewModel>();
        public ObservableCollection<OrderViewModel> OrderHistory { get; set; } = new ObservableCollection<OrderViewModel>();

        public ICommand ViewOrderDetailsCommand { get; }
        public ICommand CreateOrderCommand { get; }

        public OrdersViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            ViewOrderDetailsCommand = new RelayCommand<OrderViewModel>(ViewOrderDetails);
            CreateOrderCommand = new RelayCommand(CreateOrder);
            LoadOrders();
        }

        private void LoadOrders()
        {
            ActiveOrders.Clear();
            OrderHistory.Clear();

            try
            {
                using (var context = new PCComponentsContext())
                {
                    var activeStatuses = new[] { OrderStatus.New, OrderStatus.Processing, OrderStatus.Shipped };
                    var orders = context.Orders
                        .Where(o => o.UserId == CurrentUser.UserId)
                        .OrderByDescending(o => o.CreatedDate)
                        .ToList();

                    foreach (var order in orders)
                    {
                        var orderVm = new OrderViewModel
                        {
                            OrderId = order.OrderId,
                            OrderDate = order.CreatedDate,
                            TotalAmount = order.TotalPrice,
                            Status = order.Status.ToString(),
                            DeliveryAddress = order.DeliveryAddress,
                            ContactPhone = order.ContactPhone,
                            OrderDetails = order.OrderDetails
                        };

                        if (activeStatuses.Contains(order.Status))
                            ActiveOrders.Add(orderVm);
                        else
                            OrderHistory.Add(orderVm);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewOrderDetails(OrderViewModel order)
        {
            var detailsWindow = new Views.OrderDetailsWindow(order);
            detailsWindow.ShowDialog();
        }

        private void CreateOrder()
        {
            var createOrderWindow = new Views.CreateOrderWindow(CurrentUser, new List<ComponentEntity>());
            if (createOrderWindow.ShowDialog() == true)
            {
                LoadOrders();
            }
        }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhone { get; set; }
        public string OrderDetails { get; set; }
    }
} 