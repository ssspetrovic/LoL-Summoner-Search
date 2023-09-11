using System.Globalization;
using System.Windows.Data;
using System;
using System.Windows;

namespace LeagueSummonerSearch.Converter;

internal class ObjectToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null ? Visibility.Hidden : Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}