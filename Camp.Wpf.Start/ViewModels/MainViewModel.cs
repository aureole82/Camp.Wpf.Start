using System.ComponentModel.DataAnnotations;
using Camp.Wpf.Start.Architecture;

namespace Camp.Wpf.Start.ViewModels
{
    public class MainViewModel : ValidatedObservableModel
    {
        private string _email = "email";
        private string _greeting = "Hello, this is brought to you by the ViewModel and DataBinding.";
        private string _name = "(enter your name here)";

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
    }
}