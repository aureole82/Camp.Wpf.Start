using System;

namespace Camp.Wpf.Start.Architecture
{
    public interface IEventArgsConverter
    {
        object Convert(EventArgs args);
    }
}