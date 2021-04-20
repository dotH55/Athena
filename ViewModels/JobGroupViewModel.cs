using Athena_REST.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athena_REST.ViewModels
{
    public class JobGroupViewModel : ObservableCollection<JobClassModel>, INotifyPropertyChanged
    {
        private bool _expanded;
        public string Title { get; set; }
        public int JobItems { get; set; }
        public static ObservableCollection<JobGroupViewModel> Contents { private set; get; }
        public string StateIcon => Expanded ? "Expand_Icon.png" : "Collapse_Icon.png";
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public JobGroupViewModel(string title, bool expanded = false)
        {
            Title = title;
            Expanded = expanded;
        }

        // Might need to be async
    }
}
