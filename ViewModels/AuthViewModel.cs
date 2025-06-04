using PCConfigurator.Data;
using PCConfigurator.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public AuthViewModel()
        {
            try
            {
                var context = new PCComponentsContext();
                _authService = new AuthService(context);

                LoginCommand = new RelayCommand(Login);
                RegisterCommand = new RelayCommand(Register);
                SwitchModeCommand = new RelayCommand(() =>
                {
                    IsLoginMode = !IsLoginMode;
                    OnPropertyChanged(nameof(SubmitCommand));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // CloseAction вызывается окном при успешном входе/регистрации
        public Action CloseAction { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }  // Это поле используется только для регистрации

        // Если IsLoginMode=true – это режим входа, если false – регистрация
        private bool _isLoginMode = true;
        public bool IsLoginMode
        {
            get => _isLoginMode;
            set
            {
                if (_isLoginMode != value)
                {
                    _isLoginMode = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SubmitCommand));
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand SwitchModeCommand { get; }

        // Общая команда для отправки формы (обрабатывает либо вход, либо регистрацию)
        public ICommand SubmitCommand => IsLoginMode ? LoginCommand : RegisterCommand;

        public User CurrentUser { get; private set; }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Введите имя пользователя и пароль.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_authService == null)
                {
                    MessageBox.Show("Ошибка аутентификации: сервис не инициализирован", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                CurrentUser = _authService.Authenticate(Username, Password);

                // Если пользователь найден, но заблокирован, выдаем сообщение и прерываем вход.
                if (CurrentUser != null && CurrentUser.IsBlocked)
                {
                    MessageBox.Show("Ваш аккаунт заблокирован. Обратитесь в службу поддержки.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    CurrentUser = null;
                    return;
                }

                if (CurrentUser == null)
                {
                    MessageBox.Show("Неверные учетные данные.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    CloseAction?.Invoke();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register()
        {
            // При регистрации все поля обязательны
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Заполните все поля для регистрации.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_authService.Register(Username, Password, Email))
                {
                    MessageBox.Show("Регистрация успешна!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    using (var newContext = new PCComponentsContext())
                    {
                        var newAuthService = new AuthService(newContext);
                        CurrentUser = newAuthService.Authenticate(Username, Password);
                    }

                    if (CurrentUser != null)
                    {
                        CloseAction?.Invoke();
                    }
                    else
                    {
                        IsLoginMode = true;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь уже существует.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
