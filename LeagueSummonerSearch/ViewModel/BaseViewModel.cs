using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LeagueSummonerSearch.ViewModel;

internal class BaseViewModel : INotifyPropertyChanged
{
    protected void SetProperty<T>(ref T member, T val, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(member, val)) return;

        member = val;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged = delegate { };
}