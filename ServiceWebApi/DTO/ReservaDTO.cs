using Model;

namespace ServiceWebApi.DTO
{
    public class ReservaDTO : Reserva
    {
        public Estacionamiento Estacionamiento { get; set;  }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public byte[] FotoDePerfil { get; set; }
        public string TipoDeVehiculo { get; set; }
    }
}
