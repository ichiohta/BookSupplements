using System;
using Windows.UI.Xaml.Data;

namespace TwoWayBindingSample
{
    public class DecimalToString : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return decimal.Parse(value.ToString());
        }
    }
}
