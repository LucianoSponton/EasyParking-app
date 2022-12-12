using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    public class EstadoReservaToColorBtn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return Color.Transparent;
                case EstadoReserva.ESPERANDO_ARRIBO:
                    return Color.FromHex("#E74C3C");
                case EstadoReserva.CANCELADO:
                    return Color.Transparent;
                case EstadoReserva.ARRIBO_EXITOSO:
                    return Color.Transparent;
                case EstadoReserva.SE_HA_MARCHADO:
                    return Color.FromHex("#2968C8");

                default:
                    return Color.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}