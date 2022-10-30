using System;
using System.Globalization;
using Xamarin.Forms;


namespace EasyParking.Converter
{
    public class EstadoDePublicacionToStringoColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var PublicacionPausada = (bool)value;

            if (PublicacionPausada)
            {
                return "Black";
            }
            else
            {
                return "Gray";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
