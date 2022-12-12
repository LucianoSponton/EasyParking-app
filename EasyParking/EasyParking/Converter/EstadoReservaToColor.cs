using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    public class EstadoReservaToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return Color.FromHex("#EAEDED");
                case EstadoReserva.ESPERANDO_ARRIBO:
                    return "#EAEDED";
                case EstadoReserva.CANCELADO:
                    return Color.FromHex("#FADBD8");
                case EstadoReserva.ARRIBO_EXITOSO:
                    return Color.FromHex("#D6EAF8");
                case EstadoReserva.SE_HA_MARCHADO:
                    return Color.FromHex("#D1F2EB");
                default:
                    return Color.FromHex("#EAEDED");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}