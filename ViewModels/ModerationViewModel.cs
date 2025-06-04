using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PCConfigurator.Data;
using Microsoft.EntityFrameworkCore;

namespace PCConfigurator.ViewModels
{
    public class ModerationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ObservableCollection<ReviewViewModel> Reviews { get; set; } = new ObservableCollection<ReviewViewModel>();
        public ObservableCollection<StatusFilter> StatusFilters { get; set; } = new ObservableCollection<StatusFilter>();

        private StatusFilter _selectedStatusFilter;
        public StatusFilter SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                _selectedStatusFilter = value;
                OnPropertyChanged(nameof(SelectedStatusFilter));
                LoadReviews();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }

        public ModerationViewModel()
        {
            StatusFilters.Add(new StatusFilter { Id = 0, Name = "Все" });
            StatusFilters.Add(new StatusFilter { Id = 1, Name = "Ожидают" });
            StatusFilters.Add(new StatusFilter { Id = 2, Name = "Одобрены" });
            StatusFilters.Add(new StatusFilter { Id = 3, Name = "Отклонены" });

            SelectedStatusFilter = StatusFilters[0];

            RefreshCommand = new RelayCommand(LoadReviews);
            ApproveCommand = new RelayCommand<ReviewViewModel>(ApproveReview);
            RejectCommand = new RelayCommand<ReviewViewModel>(RejectReview);

            LoadReviews();
        }

        private void LoadReviews()
        {
            Reviews.Clear();
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var query = context.Reviews
                        .Include(r => r.User)
                        .Include(r => r.Component)
                        .AsQueryable();

                    if (SelectedStatusFilter.Id == 1) // Ожидают
                        query = query.Where(r => !r.IsApproved.HasValue);
                    else if (SelectedStatusFilter.Id == 2) // Одобрены
                        query = query.Where(r => r.IsApproved == true);
                    else if (SelectedStatusFilter.Id == 3) // Отклонены
                        query = query.Where(r => r.IsApproved == false);

                    var reviews = query.OrderByDescending(r => r.ReviewDate).ToList();

                    foreach (var review in reviews)
                    {
                        Reviews.Add(new ReviewViewModel
                        {
                            ReviewId = review.ReviewId,
                            Username = review.User.Username,
                            ComponentName = review.Component != null ? review.Component.Name : "-",
                            Content = review.Content,
                            Rating = review.Rating,
                            ReviewDate = review.ReviewDate,
                            Status = review.IsApproved == null ? "Ожидает" :
                                    review.IsApproved.Value ? "Одобрен" : "Отклонен"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отзывов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApproveReview(ReviewViewModel review)
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var reviewToUpdate = context.Reviews.FirstOrDefault(r => r.ReviewId == review.ReviewId);
                    if (reviewToUpdate != null)
                    {
                        reviewToUpdate.IsApproved = true;
                        context.SaveChanges();
                        LoadReviews();
                        MessageBox.Show("Отзыв одобрен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при одобрении отзыва: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RejectReview(ReviewViewModel review)
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var reviewToUpdate = context.Reviews.FirstOrDefault(r => r.ReviewId == review.ReviewId);
                    if (reviewToUpdate != null)
                    {
                        reviewToUpdate.IsApproved = false;
                        context.SaveChanges();
                        LoadReviews();
                        MessageBox.Show("Отзыв отклонен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отклонении отзыва: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ReviewViewModel
    {
        public int ReviewId { get; set; }
        public string Username { get; set; }
        public string ComponentName { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Status { get; set; }
    }

    public class StatusFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
} 