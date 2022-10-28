using System;
using System.Globalization;
using Xamarin.Forms;


namespace EasyParking.Converter
{
    public class TipoDeVehiculoToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (string)value;


            if (v == "Moto")
            {
                return "moto.png";
            }
            else if (v == "Auto")
            {
                return "auto.png";
            }
            else 
            {
                return "camioneta.png";
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
