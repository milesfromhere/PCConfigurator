using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModels
{
    public class ReviewsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();
        public ObservableCollection<ComponentEntity> Components { get; set; } = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<int> RatingOptions { get; } = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5 };

        private string _newReviewText;
        public string NewReviewText
        {
            get => _newReviewText;
            set { _newReviewText = value; OnPropertyChanged(nameof(NewReviewText)); }
        }

        private int _newReviewRating;
        public int NewReviewRating
        {
            get => _newReviewRating;
            set { _newReviewRating = value; OnPropertyChanged(nameof(NewReviewRating)); }
        }

        private int? _componentId;
        public int? ComponentId
        {
            get => _componentId;
            set { _componentId = value; OnPropertyChanged(nameof(ComponentId)); LoadReviews(); }
        }

        private ComponentEntity _selectedComponent;
        public ComponentEntity SelectedComponent
        {
            get => _selectedComponent;
            set { _selectedComponent = value; OnPropertyChanged(nameof(SelectedComponent)); ComponentId = value?.ComponentID; }
        }

        public User CurrentUser { get; set; }

        public ICommand AddReviewCommand { get; }
        public ICommand DeleteReviewCommand { get; }

        public ReviewsViewModel(User currentUser, int? componentId = null)
        {
            CurrentUser = currentUser;
            AddReviewCommand = new RelayCommand(AddReview);
            DeleteReviewCommand = new RelayCommand<Review>(DeleteReview);
            NewReviewRating = 5; // значение по умолчанию
            LoadComponents();
            if (componentId.HasValue)
                SelectedComponent = Components.FirstOrDefault(c => c.ComponentID == componentId.Value);
            else if (CurrentUser.IsAdmin && Components.Count > 0)
                SelectedComponent = Components[0];
            else
                ComponentId = componentId;
        }

        private void LoadReviews()
        {
            Reviews.Clear();
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var query = context.Reviews.Include("User");
                    if (ComponentId.HasValue)
                        query = query.Where(r => r.ComponentId == ComponentId.Value);
                    var reviewsFromDb = query.OrderByDescending(r => r.ReviewDate).ToList();
                    foreach (var rev in reviewsFromDb)
                        Reviews.Add(rev);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отзывов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadComponents()
        {
            Components.Clear();
            using (var context = new PCComponentsContext())
            {
                foreach (var comp in context.Components.OrderBy(c => c.Name))
                    Components.Add(comp);
            }
        }

        private void AddReview()
        {
            if (string.IsNullOrWhiteSpace(NewReviewText))
            {
                MessageBox.Show("Введите текст отзыва.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewReviewRating < 0 || NewReviewRating > 5)
            {
                MessageBox.Show("Оценка должна быть от 0 до 5.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ComponentId.HasValue || ComponentId.Value <= 0)
            {
                MessageBox.Show("Не выбран компонент для отзыва.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CurrentUser == null || CurrentUser.UserId <= 0)
            {
                MessageBox.Show("Ошибка пользователя. Перезапустите приложение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var componentExists = context.Components.Any(c => c.ComponentID == ComponentId.Value);
                    var userExists = context.Users.Any(u => u.UserId == CurrentUser.UserId);
                    if (!componentExists)
                    {
                        MessageBox.Show($"Компонент с ID={ComponentId.Value} не найден в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (!userExists)
                    {
                        MessageBox.Show($"Пользователь с ID={CurrentUser.UserId} не найден в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var review = new Review
                    {
                        UserId = CurrentUser.UserId,
                        Content = NewReviewText,
                        Rating = NewReviewRating,
                        ReviewDate = DateTime.Now,
                        ComponentId = ComponentId.Value
                    };
                    context.Reviews.Add(review);
                    context.SaveChanges();
                    review.User = context.Users.FirstOrDefault(u => u.UserId == review.UserId);
                    Reviews.Insert(0, review);
                }
                NewReviewText = string.Empty;
                NewReviewRating = 5;
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException?.Message ?? "";
                MessageBox.Show($"Ошибка добавления отзыва: {ex.Message}\n{inner}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteReview(Review review)
        {
            if (review == null) return;
            if (MessageBox.Show("Удалить этот отзыв?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new PCComponentsContext())
                    {
                        var revToDelete = context.Reviews.Find(review.ReviewId);
                        if (revToDelete != null)
                        {
                            context.Reviews.Remove(revToDelete);
                            context.SaveChanges();
                        }
                    }
                    Reviews.Remove(review);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления отзыва: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
