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
    OnPropertyChanged(propertyName);
}
```
