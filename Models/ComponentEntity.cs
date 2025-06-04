using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PCConfigurator.Data
{
    public class ComponentEntity : INotifyPropertyChanged
    {
        [Key]
        public int ComponentID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();
        public int Stock { get; set; }

        [NotMapped]
        public List<string> SpecificationsText => Specifications.Select(s => s.SpecText).ToList();

        private bool _isFavorite;

        [NotMapped]
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
