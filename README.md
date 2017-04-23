# Camp.Wpf.Start
This is a lightweight desktop application to study WPF and MVVM. It follows my experiences and recommendation having a good [Software Architecture @ Desktop](http://devsofa.blogspot.de/2017/01/software-architecture-desktop.html).

Here are some tiny but important steps I'd like to demonstrate how you achieve it. Follow and adapt it where appropriate...

## ViewModels and Views
The `MainWindow` is your first view! Add a simple class `MainViewModel` to provide a data context:

```xml
<Window ...
        xmlns:vm="clr-namespace:Camp.Wpf.Start.ViewModels">
    <Window.DataContext>
        <vm:MainViewModel />
    </Window.DataContext>
</Window>
```

That's all you need to display the properties of the ViewModel: `<TextBlock Text="{Binding Greeting}" />`

## ViewModels and INotifyPropertyChanged
Implementing `INotifyPropertyChanged` notifies the View about updates.
```cs
public class ObservableModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    /// <summary> Notifies that the property of this instance has changed. </summary>
    [Obsolete("Use SetProperty<T>()")]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
```
Derive and just invoke `OnPropertyChanged()` by the properties' setters. With the following 2nd method you can shortcut the complete setter:
```cs
/// <summary> Updates the property Notifies, if changed. Notifies the update afterwards. </summary>
protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
{
    if (Equals(field, value)) return;

    field = value;
    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
}
```

## ViewModels and INotifyDataErrorInfo
Implementing `INotifyDataErrorInfo` notifies the View about invalid data inside bound ViewModels and Models.
```cs
public class ValidatedObservableModel : ObservableModel, INotifyDataErrorInfo
{
    private readonly IDictionary<string, string[]> _errors = new Dictionary<string, string[]>();

    public IEnumerable GetErrors(string propertyName)
    {
        string[] errors;
        _errors.TryGetValue(propertyName, out errors);
        return errors ?? new string[] {};
    }

    public bool HasErrors => _errors.Any();
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

    protected override void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        base.SetProperty(ref field, value, propertyName);
        ValidateProperty(value, propertyName);
    }

    private void ValidateProperty<T>(T value, string propertyName)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateProperty(
            value,
            new ValidationContext(this) { MemberName = propertyName },
            validationResults
        );

        if (validationResults.Any())
        {
            _errors[propertyName] = validationResults
                .Select(result => result.ErrorMessage)
                .ToArray();
        }
        else
        {
            _errors.Remove(propertyName);
        }
        ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
```
Note: The usage of `OnPropertyChanged()` would bypass validation that's why I don't support it anymore. Just use `SetProperty()` furthermore!


## ViewModels and ICommand

If the ViewModel exposes `ICommand` properties you can bind them to Button.Command properties to react on Clicked events by calling methods inside the ViewModel. Here's my way of implementing a generic `ICommand` accepting a parameter (use `CommandParameter` to bind).
```cs
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
```
Just for completeness, a version without the usage of parameters:
```cs
public class RelayCommand : RelayCommand<object>
{
    /// <summary> Simple RelayCommand without evaluating the command parameter. </summary>
    public RelayCommand(Action execute, Func<bool> canExecute = null)
        : base(_ => execute(), _ => canExecute?.Invoke() ?? true)
    {
    }
}
```
