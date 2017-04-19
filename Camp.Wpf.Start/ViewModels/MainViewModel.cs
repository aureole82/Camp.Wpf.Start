using Camp.Wpf.Start.Architecture;

namespace Camp.Wpf.Start.ViewModels
{
    public class MainViewModel : ObservableModel
    {
        private string _greeting = "Hello, this is brought to you by the ViewModel and DataBinding.";
        private string _input = "(enter your name here)";

        public string Greeting
        {
            get { return _greeting; }
            set { SetProperty(ref _greeting, value); }
        }

        public string Input
        {
            get { return _input; }
            set
            {
                SetProperty(ref _input, value);
                Greeting = $"Hello, {_input}";
            }
        }
    }
}