using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PCConfigurator.Controls
{
    public partial class LanguageToggleButton : UserControl
    {
        public ICommand ToggleLanguageCommand { get; }

        public string CurrentLanguageDisplay
        {
            get => Application.Current.Resources.MergedDictionaries
                        .Any(d => d.Source?.OriginalString.Contains("ru-RU") == true)
                        ? "🇷🇺 Рус" : "🇬🇧 Eng";
        }

        public LanguageToggleButton()
        {
            InitializeComponent();
            ToggleLanguageCommand = new RelayCommand(ToggleLanguage);
            DataContext = this;
        }

        private void ToggleLanguage()
        {
            string newLanguage = CurrentLanguageDisplay.Contains("Рус") ? "en-US" : "ru-RU";
            ChangeLanguage(newLanguage);
        }

        private void ChangeLanguage(string languageCode)
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri($"Resources/Strings.{languageCode}.xaml", UriKind.Relative)
            };

            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.StartsWith("Resources/Strings."));
            if (oldDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            Application.Current.Resources.MergedDictionaries.Add(dict);
            OnPropertyChanged(nameof(CurrentLanguageDisplay));
        }

        public event EventHandler LanguageChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
