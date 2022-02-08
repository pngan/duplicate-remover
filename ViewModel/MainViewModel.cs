using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public interface IMainViewModel
    {
        void Start();
    }

    public class MainViewModel : ObservableObject, IMainViewModel
    {
        private readonly IFinderService m_model;

        public ObservableCollection<List<string>> EqualFiles { get; } = new();


        public MainViewModel(IFinderService model)
        {
            m_model = model;

            FindDuplicatesCommand = new AsyncRelayCommand (FindDuplicatesAsync);

        }

        private bool m_isBusy;


        public bool IsBusy
        {
            get { return m_isBusy; }
            set
            {
                m_isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public ICommand FindDuplicatesCommand { get; set; }

        public void Start()
        {
            
        }

        private async Task FindDuplicatesAsync()
        {
            IsBusy = true;
            var duplicateGroups = await m_model.FindDuplicatesAsync();

            EqualFiles.Clear();
            foreach (var duplicateGroup in duplicateGroups.Values.Where(g => g.Count > 1))
            {
                EqualFiles.Add(duplicateGroup);
            }
            IsBusy = false;
        }
    }
}