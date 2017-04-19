using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Camp.Wpf.Start.Architecture
{
    public class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary> Notifies that the property of this instance has changed. </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary> Updates the property Notifies, if changed. Notifies the update afterwards. </summary>
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;

            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}