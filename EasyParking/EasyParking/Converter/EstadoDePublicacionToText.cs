using System;
using System.Globalization;
using Xamarin.Forms;


namespace EasyParking.Converter
{
    class EstadoDePublicacionToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var PublicacionPausada = (bool)value;

            if (PublicacionPausada)
            {
                return "Reanudar publicación";
            }
            else
            {
                return "Publicación pausada";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
