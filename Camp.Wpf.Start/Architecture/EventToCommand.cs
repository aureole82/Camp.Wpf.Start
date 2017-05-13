using System;
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

        public static readonly DependencyProperty EventArgsConverterProperty
            = DependencyProperty.Register("EventArgsConverter", typeof(IEventArgsConverter), typeof(EventToCommand), new PropertyMetadata(default(IEventArgsConverter)));

        public IEventArgsConverter EventArgsConverter
        {
            get { return (IEventArgsConverter) GetValue(EventArgsConverterProperty); }
            set { SetValue(EventArgsConverterProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (EventArgsConverter != null)
                parameter = EventArgsConverter.Convert(parameter as EventArgs);

            if (Command?.CanExecute(parameter) != true)
                return;

            Command.Execute(parameter);
        }
    }
}