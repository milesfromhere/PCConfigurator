using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        // Добавляем свойство для ID текущего админа
        public int CurrentAdminId { get; }

        public ObservableCollection<UserDetails> Users { get; set; } = new ObservableCollection<UserDetails>();

        public ICommand ToggleBlockCommand { get; }

        public UserManagementViewModel(User currentUser)
        {
            if (currentUser == null)
                throw new ArgumentNullException(nameof(currentUser));

            CurrentAdminId = currentUser.UserId;
            LoadUsers();
            ToggleBlockCommand = new RelayCommand<UserDetails>(ToggleBlock);
        }

        private void ToggleBlock(UserDetails userDetail)
        {
            if (userDetail == null)
                return;

            // Если пользователь, которого хотят заблокировать, совпадает с текущим админом, отказываем
            if (userDetail.UserId == CurrentAdminId)
            {
                MessageBox.Show("Администратор не может блокировать самого себя!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new PCComponentsContext())
                {
                    var userEntity = context.Users.FirstOrDefault(u => u.UserId == userDetail.UserId);
                    if (userEntity != null)
                    {
                        userEntity.IsBlocked = !userEntity.IsBlocked;
                        context.SaveChanges();
                    }
                }
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления пользователя: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var usersFromDb = context.Users.ToList();

                    var userDetailsList = usersFromDb.Select(user =>
                    {
                        // Получаем сборки для данного пользователя
                        var builds = context.Builds
                            .Where(b => b.UserId == user.UserId)
                            .ToList();

                        // Получаем избранные компоненты и формируем модель деталей пользователя
                        var favorites = context.UserFavorites
                            .Include(uf => uf.Component)
                            .Where(uf => uf.UserId == user.UserId)
                            .Select(uf => uf.Component)
                            .ToList();

                        return new UserDetails
                        {
                            UserId = user.UserId,
                            Username = user.Username,
                            Email = user.Email,
                            IsBlocked = user.IsBlocked,
                            Builds = builds,
                            Favorites = favorites,
                            // Если нужно, можно передать и другие данные
                        };
                    }).ToList();

                    Users.Clear();
                    foreach (var ud in userDetailsList)
                        Users.Add(ud);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки пользователей: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Реализация INotifyPropertyChanged опущена для краткости
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
