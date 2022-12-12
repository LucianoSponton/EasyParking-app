using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    public class TextInfoToTitleCase : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (Model.Enums.Dia)value;

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(v.ToString().ToLower());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
