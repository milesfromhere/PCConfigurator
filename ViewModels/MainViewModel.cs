using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Helpers;

namespace PCConfigurator
{
    // Конвертер для отображения иконки темы

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Пользователь и административные права
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsAdmin)); }
        }

        public bool IsAdmin => CurrentUser?.IsAdmin ?? false;

        private bool _isBurgerMenuOpen;
        public bool IsBurgerMenuOpen
        {
            get => _isBurgerMenuOpen;
            set { _isBurgerMenuOpen = value; OnPropertyChanged(); }
        }

        private bool _isDarkTheme = false;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set { _isDarkTheme = value; OnPropertyChanged(); ApplyTheme(); }
        }

        private bool _isFavoritesMode;
        public bool IsFavoritesMode
        {
            get => _isFavoritesMode;
            set
            {
                _isFavoritesMode = value;
                OnPropertyChanged();
            }
        }

        private void OpenUserManagement()
        {
            var window = new Views.UserManagementWindow(CurrentUser);
            window.ShowDialog();
        }


        private void ApplyTheme()
        {
            var app = Application.Current;
            if (app == null)
                return;
            // Если выбрана тёмная тема, используем Styles.xaml, иначе LightStyles.xaml
            app.Resources.MergedDictionaries[0].Source = IsDarkTheme ?
                new Uri("Resources/Styles.xaml", UriKind.Relative) :
                new Uri("Resources/LightStyles.xaml", UriKind.Relative);
        }

        public void ChangeLanguage(string languageCode)
        {
            var dict = new ResourceDictionary();
            dict.Source = languageCode == "en-US" ?
                new Uri("Resources/Strings.en-US.xaml", UriKind.Relative) :
                new Uri("Resources/Strings.ru-RU.xaml", UriKind.Relative);

            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.StartsWith("Resources/Strings."));
            if (oldDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        // Коллекции компонентов, полученных из БД
        public ObservableCollection<ComponentEntity> FavoriteComponents { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> CPUs { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> GPUs { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> RAMs { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> Motherboards { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> PSUs { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> Storages { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> Coolers { get; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<ComponentEntity> Cases { get; } = new ObservableCollection<ComponentEntity>();

        // Свойства выбранных компонентов для конфигурации.
        // При выборе конкретного элемента его значение присваивается в SelectedComponent.
        private ComponentEntity _selectedCPU, _selectedGPU, _selectedRAM, _selectedMotherboard,
                                 _selectedPSU, _selectedStorage, _selectedCooler, _selectedCase;
        private ComponentEntity _selectedComponent;
        public ComponentEntity SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                if (_selectedComponent != value)
                {
                    _selectedComponent = value;
                    OnPropertyChanged(nameof(SelectedComponent));

                    if (value != null && value.Category != null)
                    {
                        string categoryName = value.Category.CategoryName;
                        switch (categoryName)
                        {
                            case "Процессор":
                                if (SelectedCPU != value)
                                    SelectedCPU = value;
                                break;
                            case "Видеокарта":
                                if (SelectedGPU != value)
                                    SelectedGPU = value;
                                break;
                            case "Оперативная память":
                                if (SelectedRAM != value)
                                    SelectedRAM = value;
                                break;
                            case "Материнская плата":
                                if (SelectedMotherboard != value)
                                    SelectedMotherboard = value;
                                break;
                            case "Блок питания":
                                if (SelectedPSU != value)
                                    SelectedPSU = value;
                                break;
                            case "Накопитель":
                                if (SelectedStorage != value)
                                    SelectedStorage = value;
                                break;
                            case "Кулер":
                                if (SelectedCooler != value)
                                    SelectedCooler = value;
                                break;
                            case "Корпус":
                                if (SelectedCase != value)
                                    SelectedCase = value;
                                break;
                        }
                    }
                }
            }
        }

        public ComponentEntity SelectedCPU
        {
            get => _selectedCPU;
            set
            {
                if (_selectedCPU != value)
                {
                    _selectedCPU = value;
                    OnPropertyChanged(nameof(SelectedCPU));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedGPU
        {
            get => _selectedGPU;
            set
            {
                if (_selectedGPU != value)
                {
                    _selectedGPU = value;
                    OnPropertyChanged(nameof(SelectedGPU));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedRAM
        {
            get => _selectedRAM;
            set
            {
                if (_selectedRAM != value)
                {
                    _selectedRAM = value;
                    OnPropertyChanged(nameof(SelectedRAM));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedMotherboard
        {
            get => _selectedMotherboard;
            set
            {
                if (_selectedMotherboard != value)
                {
                    _selectedMotherboard = value;
                    OnPropertyChanged(nameof(SelectedMotherboard));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedPSU
        {
            get => _selectedPSU;
            set
            {
                if (_selectedPSU != value)
                {
                    _selectedPSU = value;
                    OnPropertyChanged(nameof(SelectedPSU));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedStorage
        {
            get => _selectedStorage;
            set
            {
                if (_selectedStorage != value)
                {
                    _selectedStorage = value;
                    OnPropertyChanged(nameof(SelectedStorage));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedCooler
        {
            get => _selectedCooler;
            set
            {
                if (_selectedCooler != value)
                {
                    _selectedCooler = value;
                    OnPropertyChanged(nameof(SelectedCooler));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }
        public ComponentEntity SelectedCase
        {
            get => _selectedCase;
            set
            {
                if (_selectedCase != value)
                {
                    _selectedCase = value;
                    OnPropertyChanged(nameof(SelectedCase));
                    UpdateConfiguration();
                    if (value != null)
                        SelectedComponent = value;
                }
            }
        }

        // Коллекция текущей конфигурации и итоговая цена
        public ObservableCollection<ComponentEntity> CurrentConfiguration { get; } = new ObservableCollection<ComponentEntity>();
        public decimal TotalPrice => CurrentConfiguration.Sum(c => c?.Price ?? 0);

        // Команды приложения
        public ICommand OpenUserManagementCommand { get; }
        public ICommand OpenProfileCommand { get; }
        public ICommand OpenComponentsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand RemoveComponentCommand { get; }
        public ICommand ChangeThemeCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ToggleFavoritesViewCommand { get; }
        public ICommand OpenModerationCommand { get; }
        public ICommand OpenAllOrdersCommand { get; }
        public ICommand OpenReportsCommand { get; }
        public ICommand OpenCatalogCommand { get; }
        public ICommand OpenReviewsCommand { get; }
        public ICommand OpenFavoritesCommand { get; }
        public ICommand OpenAuthCommand { get; }
        public ICommand OpenOrderCommand { get; }
        public ICommand OpenOrderHistoryCommand { get; }

        // Для истории выбранных компонентов
        private List<ComponentEntity> _selectedComponents = new List<ComponentEntity>();
        public IEnumerable<ComponentEntity> SelectedComponents => _selectedComponents;

        public MainViewModel(User user)
        {
            CurrentUser = user;
            OpenUserManagementCommand = new RelayCommand(OpenUserManagement, () => IsAdmin);
            AddToCartCommand = new RelayCommand(AddSelectedComponentToCart);
            LoadComponentsFromDB();
            LoadFavoriteComponents();
            OpenComponentsCommand = new RelayCommand(OpenComponentsWindow);
            SaveCommand = new RelayCommand(SaveConfiguration);
            ResetCommand = new RelayCommand(ResetConfiguration);
            RemoveComponentCommand = new RelayCommand<ComponentEntity>(RemoveComponent);
            ChangeThemeCommand = new RelayCommand(ToggleTheme);
            ChangeLanguageCommand = new RelayCommand<string>(ChangeLanguage);
            OpenProfileCommand = new RelayCommand(OpenProfile);
            ToggleFavoriteCommand = new RelayCommand<ComponentEntity>(ToggleFavorite);
            ToggleFavoritesViewCommand = new RelayCommand(() => { IsFavoritesMode = !IsFavoritesMode; });
            OpenModerationCommand = new RelayCommand(OpenModeration);
            OpenAllOrdersCommand = new RelayCommand(OpenAllOrders);
            OpenReportsCommand = new RelayCommand(OpenReports);
            OpenCatalogCommand = new RelayCommand(OpenCatalog);
            OpenReviewsCommand = new RelayCommand(OpenReviews);
            OpenFavoritesCommand = new RelayCommand(OpenFavorites);
            OpenAuthCommand = new RelayCommand(OpenAuth);
            OpenOrderCommand = new RelayCommand(OpenOrder);
            OpenOrderHistoryCommand = new RelayCommand(OpenOrderHistory);
        }

        /// <summary>
        /// Загружает избранные компоненты для текущего пользователя из таблицы UserFavorites.
        /// </summary>
        private void LoadFavoriteComponents()
        {
            FavoriteComponents.Clear();
            if (CurrentUser == null)
                return;
            using (var context = new PCComponentsContext())
            {
                var favComponents = context.UserFavorites
                    .Include(uf => uf.Component)
                        .ThenInclude(c => c.Category)
                    .Include(uf => uf.Component)
                        .ThenInclude(c => c.Specifications)
                    .Where(uf => uf.UserId == CurrentUser.UserId)
                    .Select(uf => uf.Component)
                    .ToList();

                foreach (var comp in favComponents)
                {
                    comp.IsFavorite = true;
                    FavoriteComponents.Add(comp);
                }
            }
        }

        public void RefreshSelectedComponent()
        {
            // Принудительно поднимаем событие обновления SelectedComponent.
            OnPropertyChanged(nameof(SelectedComponent));
        }
        private void ToggleFavorite(ComponentEntity component)
        {
            if (component == null || CurrentUser == null)
                return;

            using (var context = new PCComponentsContext())
            {
                var favorite = context.UserFavorites.FirstOrDefault(
                    uf => uf.UserId == CurrentUser.UserId && uf.ComponentID == component.ComponentID);
                if (favorite != null)
                {
                    // Удаляем связь, если компонент уже в избранном
                    context.UserFavorites.Remove(favorite);
                    context.SaveChanges();
                    component.IsFavorite = false;
                    FavoriteComponents.Remove(
                        FavoriteComponents.FirstOrDefault(fc => fc.ComponentID == component.ComponentID));
                }
                else
                {
                    // Добавляем новую связь в избранное
                    var newFavorite = new UserFavorite
                    {
                        UserId = CurrentUser.UserId,
                        ComponentID = component.ComponentID
                    };
                    context.UserFavorites.Add(newFavorite);
                    context.SaveChanges();
                    component.IsFavorite = true;
                    FavoriteComponents.Add(component);
                }
                // Добавляем уведомление об изменении свойства IsFavorite
                OnPropertyChanged(nameof(component.IsFavorite));
                OnPropertyChanged(nameof(FavoriteComponents));
            }
        }

        private void OpenProfile()
        {
            var profileWindow = new Views.ProfileWindow
            {
                DataContext = new ViewModels.ProfileViewModel(CurrentUser)
            };
            profileWindow.ShowDialog();
        }

        private void LoadComponentsFromDB()
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    // Получаем все компоненты с привязанными категориями и характеристиками.
                    var comps = context.Components
                        .Include(c => c.Category)
                        .Include(c => c.Specifications)
                        .ToList();

                    // Получаем ID компонентов, добавленных в избранное текущего пользователя.
                    var favIds = new HashSet<int>(context.UserFavorites
                        .Where(uf => uf.UserId == CurrentUser.UserId)
                        .Select(uf => uf.ComponentID));

                    // Очищаем коллекции для каждой категории.
                    CPUs.Clear();
                    GPUs.Clear();
                    RAMs.Clear();
                    Motherboards.Clear();
                    PSUs.Clear();
                    Storages.Clear();
                    Coolers.Clear();
                    Cases.Clear();

                    foreach (var comp in comps)
                    {
                        // Если компонент есть в избранном, выставляем флаг.
                        comp.IsFavorite = favIds.Contains(comp.ComponentID);

                        // Добавляем в нужную коллекцию по категории.
                        switch (comp.Category?.CategoryName)
                        {
                            case "Процессор":
                                CPUs.Add(comp); break;
                            case "Видеокарта":
                                GPUs.Add(comp); break;
                            case "Оперативная память":
                                RAMs.Add(comp); break;
                            case "Материнская плата":
                                Motherboards.Add(comp); break;
                            case "Блок питания":
                                PSUs.Add(comp); break;
                            case "Накопитель":
                                Storages.Add(comp); break;
                            case "Кулер":
                                Coolers.Add(comp); break;
                            case "Корпус":
                                Cases.Add(comp); break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки компонентов: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateConfiguration()
        {
            CurrentConfiguration.Clear();
            if (SelectedCPU != null) CurrentConfiguration.Add(SelectedCPU);
            if (SelectedGPU != null) CurrentConfiguration.Add(SelectedGPU);
            if (SelectedRAM != null) CurrentConfiguration.Add(SelectedRAM);
            if (SelectedMotherboard != null) CurrentConfiguration.Add(SelectedMotherboard);
            if (SelectedPSU != null) CurrentConfiguration.Add(SelectedPSU);
            if (SelectedStorage != null) CurrentConfiguration.Add(SelectedStorage);
            if (SelectedCooler != null) CurrentConfiguration.Add(SelectedCooler);
            if (SelectedCase != null) CurrentConfiguration.Add(SelectedCase);
            OnPropertyChanged(nameof(CurrentConfiguration));
            OnPropertyChanged(nameof(TotalPrice));
        }

        private void AddSelectedComponentToCart()
        {
            if (SelectedComponent != null)
            {
                UpdateConfiguration();
                AddToCart(SelectedComponent);
            }
        }

        private void ToggleTheme() => IsDarkTheme = !IsDarkTheme;

        private void OpenComponentsWindow()
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Только администраторы могут управлять компонентами", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var window = new ComponentsWindow();
            window.Closed += (s, e) => RefreshData();
            window.ShowDialog();
        }

        public void RefreshData()
        {
            LoadComponentsFromDB();
            LoadFavoriteComponents();
        }

        private void ResetConfiguration()
        {
            SelectedCPU = SelectedGPU = SelectedRAM = SelectedMotherboard =
            SelectedPSU = SelectedStorage = SelectedCooler = SelectedCase = null;
            SelectedComponent = null;
            CurrentConfiguration.Clear();
            OnPropertyChanged(nameof(CurrentConfiguration));
            OnPropertyChanged(nameof(TotalPrice));
        }

        private int ExtractTdp(ComponentEntity component)
        {
            if (component == null) return 0;
            var tdpSpec = component.SpecificationsText.FirstOrDefault(s => s.TrimStart().StartsWith("TDP", StringComparison.OrdinalIgnoreCase));
            if (tdpSpec == null) return 0;
            // Пример: "TDP: 125W" или "TDP: 220W"
            var digits = new string(tdpSpec.Where(char.IsDigit).ToArray());
            if (int.TryParse(digits, out int tdp))
                return tdp;
            return 0;
        }

        private int ExtractPsuPower(ComponentEntity component)
        {
            if (component == null) return 0;
            // Ищем спецификацию, содержащую 'W' и не 'TDP' (например, '750W')
            var powerSpec = component.SpecificationsText.FirstOrDefault(s =>
                s.Trim().EndsWith("W", StringComparison.OrdinalIgnoreCase) &&
                !s.TrimStart().StartsWith("TDP", StringComparison.OrdinalIgnoreCase));
            if (powerSpec == null) return 0;
            var digits = new string(powerSpec.Where(char.IsDigit).ToArray());
            if (int.TryParse(digits, out int power))
                return power;
            return 0;
        }

        private string ExtractFormFactor(ComponentEntity component)
        {
            if (component == null) return null;
            // Ищем спецификацию, содержащую 'Форм‑фактор' или 'Form Factor'
            var formSpec = component.SpecificationsText.FirstOrDefault(s =>
                s.IndexOf("Форм‑фактор", StringComparison.OrdinalIgnoreCase) >= 0 ||
                s.IndexOf("Form Factor", StringComparison.OrdinalIgnoreCase) >= 0);
            if (formSpec == null) return null;
            // Пример: 'Форм‑фактор: ATX' или 'Form Factor: ATX'
            var parts = formSpec.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return null;
            return parts[1].Trim().ToUpper();
        }
        private string ExtractCaseType(ComponentEntity component)
        {
            if (component == null) return null;
            // Ищем спецификацию, содержащую 'Форм‑фактор' или 'Form Factor'
            var formSpec = component.SpecificationsText.FirstOrDefault(s =>
                s.IndexOf("Форм‑фактор", StringComparison.OrdinalIgnoreCase) >= 0 ||
                s.IndexOf("Form Factor", StringComparison.OrdinalIgnoreCase) >= 0);
            if (formSpec == null) return null;
            // Пример: 'Форм‑фактор: Mid-Tower' или 'Form Factor: Mid-Tower'
            var parts = formSpec.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return null;
            return parts[1].Trim().ToUpper();
        }

        private string ExtractFormFactorValue(ComponentEntity component)
        {
            if (component == null) return null;
            var formSpec = component.SpecificationsText.FirstOrDefault(s =>
                s.IndexOf("Форм‑фактор", StringComparison.OrdinalIgnoreCase) >= 0 ||
                s.IndexOf("Form Factor", StringComparison.OrdinalIgnoreCase) >= 0);
            if (formSpec == null) return null;
            var idx = formSpec.IndexOf(':');
            if (idx < 0 || idx == formSpec.Length - 1) return null;
            return formSpec.Substring(idx + 1).Trim().ToUpper();
        }

        private void SaveConfiguration()
        {
            // Проверка совместимости CPU и материнской платы
            if (SelectedCPU != null && SelectedMotherboard != null && !BuildValidator.IsCompatible(SelectedCPU, SelectedMotherboard))
            {
                MessageBox.Show("Сокет процессора и материнской платы не совпадают! Сборка не может быть сохранена.", "Несовместимость", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка охлаждения для мощных процессоров
            int cpuTdp = ExtractTdp(SelectedCPU);
            int coolerTdp = ExtractTdp(SelectedCooler);
            if (cpuTdp > 100 && (SelectedCooler == null || coolerTdp < 250))
            {
                MessageBox.Show("Для этого процессора рекомендуется охлаждение с поддержкой не менее 250W!", "Рекомендация по охлаждению", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка мощности БП для мощных видеокарт
            int gpuTdp = ExtractTdp(SelectedGPU);
            int psuPower = ExtractPsuPower(SelectedPSU);
            if (gpuTdp >= 250 && (SelectedPSU == null || psuPower < 700))
            {
                MessageBox.Show("Для этой видеокарты рекомендуется блок питания не менее 700W!", "Рекомендация по питанию", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка совместимости ATX и Mid Tower
            string mbForm = ExtractFormFactorValue(SelectedMotherboard);
            string caseForm = ExtractFormFactorValue(SelectedCase);
            if (mbForm == "ATX" && caseForm == "MID-TOWER")
            {
                MessageBox.Show("ATX материнская плата несовместима с корпусом Mid-Tower!", "Несовместимость корпуса", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!CurrentConfiguration.Any())
            {
                MessageBox.Show("Выберите хотя бы один компонент!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string config = string.Join("\n", CurrentConfiguration.Select(c => $"{c.Name} - {c.Price} BYN"));
            decimal totalPrice = CurrentConfiguration.Sum(c => c?.Price ?? 0);

            try
            {
                using (var context = new PCComponentsContext())
                {
                    var build = new Data.Build
                    {
                        UserId = CurrentUser.UserId,
                        BuildSummary = config,
                        TotalPrice = totalPrice,
                        CreatedDate = DateTime.Now
                    };
                    context.Builds.Add(build);
                    context.SaveChanges();
                }
                MessageBox.Show($"Конфигурация сохранена!\n\n{config}\n\nИтого: {totalPrice} BYN",
                   "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveComponent(ComponentEntity component)
        {
            if (component == null)
                return;

            if (component == SelectedCPU)
                SelectedCPU = null;
            else if (component == SelectedGPU)
                SelectedGPU = null;
            else if (component == SelectedRAM)
                SelectedRAM = null;
            else if (component == SelectedMotherboard)
                SelectedMotherboard = null;
            else if (component == SelectedPSU)
                SelectedPSU = null;
            else if (component == SelectedStorage)
                SelectedStorage = null;
            else if (component == SelectedCooler)
                SelectedCooler = null;
            else if (component == SelectedCase)
                SelectedCase = null;

            CurrentConfiguration.Remove(component);
            OnPropertyChanged(nameof(CurrentConfiguration));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void AddToCart(ComponentEntity component)
        {
            if (component != null && !_selectedComponents.Contains(component))
            {
                _selectedComponents.Add(component);
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private void OpenModeration() => MessageBox.Show("Окно модерации объявлений (заглушка)");
        private void OpenAllOrders() => MessageBox.Show("Окно всех заказов (заглушка)");
        private void OpenReports()
        {
            var reportsWindow = new Views.ReportsWindow();
            reportsWindow.ShowDialog();
        }
        private void OpenCatalog() => MessageBox.Show("Окно каталога комплектующих (заглушка)");
        private void OpenReviews() => MessageBox.Show("Окно отзывов (заглушка)");
        private void OpenFavorites() => MessageBox.Show("Окно избранного (заглушка)");
        private void OpenAuth() => MessageBox.Show("Окно регистрации/авторизации (заглушка)");
        private void OpenOrder()
        {
            // Проверка совместимости CPU и материнской платы
            if (SelectedCPU != null && SelectedMotherboard != null && !BuildValidator.IsCompatible(SelectedCPU, SelectedMotherboard))
            {
                MessageBox.Show("Сокет процессора и материнской платы не совпадают! Заказ невозможен.", "Несовместимость", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка охлаждения для мощных процессоров
            int cpuTdp = ExtractTdp(SelectedCPU);
            int coolerTdp = ExtractTdp(SelectedCooler);
            if (cpuTdp > 100 && (SelectedCooler == null || coolerTdp < 250))
            {
                MessageBox.Show("Для этого процессора рекомендуется охлаждение с поддержкой не менее 250W!", "Рекомендация по охлаждению", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка мощности БП для мощных видеокарт
            int gpuTdp = ExtractTdp(SelectedGPU);
            int psuPower = ExtractPsuPower(SelectedPSU);
            if (gpuTdp >= 250 && (SelectedPSU == null || psuPower < 700))
            {
                MessageBox.Show("Для этой видеокарты рекомендуется блок питания не менее 700W!", "Рекомендация по питанию", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка совместимости ATX и Mid Tower
            string mbForm = ExtractFormFactorValue(SelectedMotherboard);
            string caseForm = ExtractFormFactorValue(SelectedCase);
            if (mbForm == "ATX" && caseForm == "MID-TOWER")
            {
                MessageBox.Show("ATX материнская плата несовместима с корпусом Mid-Tower!", "Несовместимость корпуса", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var orderWindow = new Views.CreateOrderWindow(CurrentUser, CurrentConfiguration.ToList());
            if (orderWindow.ShowDialog() == true)
            {
                ResetConfiguration();
                MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OpenOrderHistory()
        {
            var window = new Views.OrderHistoryWindow(CurrentUser.UserId);
            window.ShowDialog();
        }
    }
}