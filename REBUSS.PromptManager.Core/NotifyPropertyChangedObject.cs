using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if(propertyName is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }

        if (typeof(T).IsValueType || typeof(T) == typeof(string))
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
        }
        else if ((object)storage == (object)value)
        {
            return false;
        }

        storage = value;
        RaiseNotifyPropertyChangedEvent(propertyName);
        return true;
    }

    protected void RaiseNotifyPropertyChangedEvent([CallerMemberName] string? propertyName = null)
    {
        if(propertyName is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }

        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}