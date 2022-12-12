using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    class EstadoReservaToCodigoDeValidacionIsVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return false;
                case EstadoReserva.ESPERANDO_ARRIBO:
                    return true;
                case EstadoReserva.CANCELADO:
                    return false;
                case EstadoReserva.ARRIBO_EXITOSO:
                    return false;
                case EstadoReserva.SE_HA_MARCHADO:
                    return false;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}