using System;
using System.Globalization;
using Xamarin.Forms;


namespace EasyParking.Converter
{
    public class DistanceToTextDisctance : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var distancia = (double)value;

            if (distancia >= 1)
            {
                return "a " + String.Format("{0:N1}", distancia) + " km";
            }
            else
            {
                distancia = distancia * 1000;
                return "a " + String.Format("{0:N1}", distancia) + " mts";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}