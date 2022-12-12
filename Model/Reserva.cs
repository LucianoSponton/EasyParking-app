using Model.Enums;
using System;

namespace Model
{
    public class Reserva
    {
        public Reserva()
        {
            FechaDeCreacion = DateTime.Now;
            DateTime fechacreacion = FechaDeCreacion;
            fechacreacion = fechacreacion.AddMinutes(20);
            FechaDeExpiracion = fechacreacion;
        }

      
        public int Id { get; set; }
        public int EstacionamientoId { get; set; }
        public string UserId { get; set; } // del cliente
        public int VehiculoId { get; set; }
        public decimal Monto { get; set; } // precio de la reserva si es que tiene 
        public string Patente { get; set; }
        public string CodigoDeValidacion { get; set; } // precio de la reserva si es que tiene 
        public EstadoReserva Estado { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeExpiracion { get; set; }

    }
}
