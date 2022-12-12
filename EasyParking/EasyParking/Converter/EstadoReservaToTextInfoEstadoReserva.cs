using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    public class EstadoReservaToTextInfoEstadoReserva : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return "";
                case EstadoReserva.ESPERANDO_ARRIBO:
                    return "Esperando tu arribo al lugar";
                case EstadoReserva.CANCELADO:
                    return "Cancelado";
                case EstadoReserva.ARRIBO_EXITOSO:
                    return "Tu vehículo esta aquí";
                case EstadoReserva.SE_HA_MARCHADO:
                    return "Estuviste aquí";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
