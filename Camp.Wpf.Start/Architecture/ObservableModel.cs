using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Camp.Wpf.Start.Architecture
{
    public class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary> Updates the property Notifies, if changed. Notifies the update afterwards. </summary>
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;

            field = value;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}