using System;
using System.Windows.Input;

namespace Camp.Wpf.Start.Architecture
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        /// <summary>
        ///     Creates a command wrapped around the given execute method. If canExecute is given,
        ///     it controls the IsEnabled state of the bound control.
        /// </summary>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((T) parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        /// <summary> Simple RelayCommand without evaluating the command parameter. </summary>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(_ => execute(), _ => canExecute?.Invoke() ?? true)
        {
        }
    }
}