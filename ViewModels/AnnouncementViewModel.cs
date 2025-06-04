using PCConfigurator.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PCConfigurator.ViewModels
{
    public class AnnouncementsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ObservableCollection<Announcement> Announcements { get; set; } = new ObservableCollection<Announcement>();

        public ICommand ApproveCommand { get; }
        public ICommand DeleteCommand { get; }

        public AnnouncementsViewModel()
        {
            LoadAnnouncements();
            ApproveCommand = new RelayCommand<Announcement>(ApproveAnnouncement);
            DeleteCommand = new RelayCommand<Announcement>(DeleteAnnouncement);
        }

        private void LoadAnnouncements()
        {
            Announcements.Clear();
            using (var context = new PCComponentsContext())
            {
                var list = context.Announcements.OrderByDescending(a => a.CreatedDate).ToList();
                foreach (var ann in list)
                    Announcements.Add(ann);
            }
        }

        private void ApproveAnnouncement(Announcement announcement)
        {
            if (announcement == null) return;
            using (var context = new PCComponentsContext())
            {
                var ann = context.Announcements.Find(announcement.AnnouncementId);
                if (ann != null)
                {
                    ann.IsApproved = true;
                    context.SaveChanges();
                }
            }
            LoadAnnouncements();
        }

        private void DeleteAnnouncement(Announcement announcement)
        {
            if (announcement == null) return;
            using (var context = new PCComponentsContext())
            {
                var ann = context.Announcements.Find(announcement.AnnouncementId);
                if (ann != null)
                {
                    context.Announcements.Remove(ann);
                    context.SaveChanges();
                }
            }
            LoadAnnouncements();
        }
    }
}
