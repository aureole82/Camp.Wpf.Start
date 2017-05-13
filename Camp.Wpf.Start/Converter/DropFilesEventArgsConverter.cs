using System;
using System.Windows;
using Camp.Wpf.Start.Architecture;

namespace Camp.Wpf.Start.Converter
{
    public class DropFilesEventArgsConverter : IEventArgsConverter
    {
        public object Convert(EventArgs args)
        {
            var dragEventArgs = args as DragEventArgs;

            var fileNames = dragEventArgs?.Data.GetData(DataFormats.FileDrop) as string[];
            return fileNames ?? new string[0];
        }
    }
}