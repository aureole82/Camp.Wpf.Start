using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Camp.Wpf.Start.Architecture
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty
            = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommand), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (Command?.CanExecute(parameter) != true) return;
            Command.Execute(parameter);
        }
    }
}