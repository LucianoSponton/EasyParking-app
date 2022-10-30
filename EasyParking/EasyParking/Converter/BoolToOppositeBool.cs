using System;
using System.Globalization;
using Xamarin.Forms;


namespace EasyParking.Converter
{
    class BoolToOppositeBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;

            if (v == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
