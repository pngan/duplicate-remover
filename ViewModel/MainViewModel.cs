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
        private readonly IMessageViewModel m_messageDialogViewModel;



        public ObservableCollection<List<string>> EqualFiles { get; } = new();


        public MainViewModel(IFinderService model, IMessageViewModel messageDialogViewModel)
        {
            m_model = model;
            m_messageDialogViewModel = messageDialogViewModel;
            m_model.OnGreetingChanged += OnGreetingChanged;

            FindDuplicatesCommand = new AsyncRelayCommand (FindDuplicatesAsync);

        }

        private void OnGreetingChanged(object sender, GreetingArgs e)
        {
            Greeting = e.Greeting;
        }

        private string m_name;
        private string m_greeting;
        private bool m_isBusy;

        public string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                m_model.SetName(value);
            }
        }

        public string Greeting
        {
            get { return m_greeting; }
            set
            {
                m_greeting = value;
                OnPropertyChanged("Greeting");
            }
        }


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