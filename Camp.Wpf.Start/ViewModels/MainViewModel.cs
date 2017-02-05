using System.Diagnostics;

namespace Camp.Wpf.Start.ViewModels
{
    public class MainViewModel
    {
        private string _input = "(enter your name here)";
        public string Greeting { get; set; } = "Hello, this is brought to you by the ViewModel and DataBinding.";

        public string Input
        {
            get { return _input; }
            set
            {
                Debug.WriteLine($@"{nameof(Input)} changed: Old=""{_input}"", New=""{value}""");
                _input = value;
            }
        }
    }
}