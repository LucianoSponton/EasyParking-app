using Model.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EasyParking.Converter
{
    public class EstadoReservaToTextBtn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (EstadoReserva)value;

            switch (v)
            {
                case EstadoReserva.NONE:
                    return "";
                case EstadoReserva.ESPERANDO_ARRIBO: // esperando arribo
                    return "Cancelar";
                case EstadoReserva.CANCELADO:
                    return "";
                case EstadoReserva.ARRIBO_EXITOSO: // tu vehiculo esta aqui
                    return "";
                case EstadoReserva.SE_HA_MARCHADO: // estuviste aqui
                    return "Reservar";
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