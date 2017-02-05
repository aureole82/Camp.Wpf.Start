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