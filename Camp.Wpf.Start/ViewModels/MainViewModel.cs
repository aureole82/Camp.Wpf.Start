using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Camp.Wpf.Start.Architecture;

namespace Camp.Wpf.Start.ViewModels
{
    public class MainViewModel : ValidatedObservableModel
    {
        private string _clickMessage = "Click me";
        private string _email = "email";
        private string _greeting = "Hello, this is brought to you by the ViewModel and DataBinding.";
        private string _name = "(enter your name here)";

        public MainViewModel()
        {
            ClickedCommand = new RelayCommand(Clicked, CanClick);
        }

        public string Greeting
        {
            get { return _greeting; }
            set { SetProperty(ref _greeting, value); }
        }

        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        [Required]
        [MinLength(3)]
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                Greeting = $"Hello, {_name}";
            }
        }

        public string ClickMessage
        {
            get { return _clickMessage; }
            set { SetProperty(ref _clickMessage, value); }
        }

        public ICommand ClickedCommand { get; }

        private bool CanClick()
        {
            return !HasErrors;
        }

        private void Clicked()
        {
            ClickMessage = "Thanks!";
        }
    }
}