using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public User CurrentUser { get; set; }
        public Action CloseAction { get; set; }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set { _repeatPassword = value; OnPropertyChanged(nameof(RepeatPassword)); }
        }

        public ObservableCollection<Build> SavedBuilds { get; set; } = new ObservableCollection<Build>();

        public ICommand SaveAccountCommand { get; }
        public ICommand DeleteAccountCommand { get; }
        public ICommand SendBuildToEmailCommand { get; }
        public ICommand RefreshBuildsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand DeleteBuildCommand { get; }

        public ProfileViewModel(User user)
        {
            CurrentUser = user;
            LoadSavedBuilds();

            SaveAccountCommand = new RelayCommand(SaveAccount, CanSaveAccount);
            DeleteAccountCommand = new RelayCommand(DeleteAccount);
            RefreshBuildsCommand = new RelayCommand(LoadSavedBuilds);
            LogoutCommand = new RelayCommand(Logout);
            DeleteBuildCommand = new RelayCommand<Build>(DeleteBuild);
        }

        private void Logout()
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CloseAction?.Invoke();
                var authWindow = new Views.AuthWindow();
                authWindow.Show();
            }
        }

        private bool CanSaveAccount()
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Username) ||
                string.IsNullOrWhiteSpace(CurrentUser.Email))
                return false;
            if (!string.IsNullOrEmpty(NewPassword) || !string.IsNullOrEmpty(RepeatPassword))
            {
                if (NewPassword != RepeatPassword)
                    return false;
                if (NewPassword.Length < 6)
                    return false;
            }
            return true;
        }

        private void SaveAccount()
        {
            if (!CanSaveAccount())
            {
                MessageBox.Show("Проверьте правильность введённых данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var userFromDb = context.Users.FirstOrDefault(u => u.UserId == CurrentUser.UserId);
                    if (userFromDb != null)
                    {
                        userFromDb.Username = CurrentUser.Username;
                        userFromDb.Name = CurrentUser.Name;
                        userFromDb.Email = CurrentUser.Email;
                        if (!string.IsNullOrEmpty(NewPassword))
                        {
                            userFromDb.Password = NewPassword;
                        }
                        userFromDb.AvatarPath = CurrentUser.AvatarPath;
                        context.SaveChanges();
                        MessageBox.Show("Данные аккаунта обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAccount()
        {
            if (MessageBox.Show("Вы действительно хотите удалить аккаунт?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new PCComponentsContext())
                    {
                        var userFromDb = context.Users.FirstOrDefault(u => u.UserId == CurrentUser.UserId);
                        if (userFromDb != null)
                        {
                            var builds = context.Builds.Where(b => b.UserId == CurrentUser.UserId).ToList();
                            if (builds.Any())
                            {
                                context.Builds.RemoveRange(builds);
                            }
                            var reviews = context.Reviews.Where(r => r.UserId == CurrentUser.UserId).ToList();
                            if (reviews.Any())
                            {
                                context.Reviews.RemoveRange(reviews);
                            }
                            var favorites = context.UserFavorites.Where(uf => uf.UserId == CurrentUser.UserId).ToList();
                            if (favorites.Any())
                            {
                                context.UserFavorites.RemoveRange(favorites);
                            }
                            context.Users.Remove(userFromDb);
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    MessageBox.Show("Аккаунт удалён.", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                        foreach (Window win in Application.Current.Windows.Cast<Window>().ToList())
                        {
                            win.Close();
                        }
                        var authWindow = new Views.AuthWindow();
                        bool? authResult = authWindow.ShowDialog();
                        var authVM = authWindow.DataContext as AuthViewModel;
                        if (authVM?.CurrentUser != null)
                        {
                            var mainWindow = new MainWindow(authVM.CurrentUser);
                            Application.Current.MainWindow = mainWindow;
                            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                            mainWindow.Show();
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadSavedBuilds()
        {
            SavedBuilds.Clear();
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var builds = context.Builds
                        .Where(b => b.UserId == CurrentUser.UserId)
                        .OrderByDescending(b => b.CreatedDate)
                        .ToList();
                    foreach (var b in builds)
                    {
                        SavedBuilds.Add(b);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сборок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string _avatarPath;
        public string AvatarPath
        {
            get => _avatarPath;
            set
            {
                _avatarPath = value;
                OnPropertyChanged(nameof(AvatarPath));
            }
        }

        public async Task SendBuildsToEmailAsync()
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("PC Configurator", "pcconfiguratorwpf@outlook.com"));
                message.To.Add(new MailboxAddress("", CurrentUser.Email));
                message.Subject = "Ваши сохранённые сборки";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = string.Join("\n\n", SavedBuilds.Select(b =>
                        $"Сборка #{b.BuildId} от {b.CreatedDate:d}\n{b.BuildSummary}\nИтого: {b.TotalPrice} BYN"))
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("pcconfiguratorwpf@outlook.com", "PCConfigurator");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                MessageBox.Show("Сборки успешно отправлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки письма: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveAvatar()
        {
            if (CurrentUser == null || CurrentUser.UserId <= 0)
            {
                MessageBox.Show("Пользователь не определён или имеет некорректный идентификатор.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new PCComponentsContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.UserId == CurrentUser.UserId);
                    if (user != null)
                    {
                        user.AvatarPath = AvatarPath ?? "C:\\Users\\nikit\\OneDrive\\Рабочий стол\\PCConfigurator\\Images\\placeholder.png";
                        context.SaveChanges();
                        OnPropertyChanged(nameof(AvatarPath));
                        CurrentUser.AvatarPath = user.AvatarPath;
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден в базе данных.",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении аватарки: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBuild(Build build)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту сборку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new PCComponentsContext())
                    {
                        var buildToDelete = context.Builds.FirstOrDefault(b => b.BuildId == build.BuildId);
                        if (buildToDelete != null)
                        {
                            context.Builds.Remove(buildToDelete);
                            context.SaveChanges();
                            SavedBuilds.Remove(build);
                            MessageBox.Show("Сборка успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении сборки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Реализация IDataErrorInfo: добавлена проверка для NewPassword и RepeatPassword
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(CurrentUser.Username):
                        if (string.IsNullOrWhiteSpace(CurrentUser.Username))
                            error = "Введите логин";
                        break;
                    case nameof(CurrentUser.Email):
                        if (string.IsNullOrWhiteSpace(CurrentUser.Email))
                            error = "Введите Email";
                        else if (!CurrentUser.Email.Contains("@"))
                            error = "Введите корректный Email";
                        break;
                    case nameof(NewPassword):
                        if (!string.IsNullOrWhiteSpace(NewPassword) && NewPassword.Length < 6)
                            error = "Пароль должен содержать минимум 6 символов.";
                        break;
                    case nameof(RepeatPassword):
                        if (!string.IsNullOrEmpty(NewPassword) && NewPassword != RepeatPassword)
                            error = "Пароли не совпадают.";
                        break;
                }
                return error;
            }
        }
    }
}
