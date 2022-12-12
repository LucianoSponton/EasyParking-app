using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;
namespace EasyParking.Converter
{
    class EstadoReservaToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return Color.FromHex("#EAEDED");
                case EstadoReserva.ESPERANDO_ARRIBO:
                    return "hourglass.png";
                case EstadoReserva.CANCELADO:
                    return "cancelado.png";
                case EstadoReserva.ARRIBO_EXITOSO:
                    return "ubicacion.png";
                case EstadoReserva.SE_HA_MARCHADO:
                    return "checkEstado.png";
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