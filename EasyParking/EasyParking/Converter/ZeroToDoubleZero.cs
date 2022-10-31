using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    class ZeroToDoubleZero : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (int)value;

            if (v == 0)
            {
                return "00";
            }
            else
            {
                return v.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
