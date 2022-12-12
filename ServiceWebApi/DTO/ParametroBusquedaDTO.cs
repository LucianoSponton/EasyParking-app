using System.Collections.Generic;

namespace ServiceWebApi.DTO
{
    public class ParametroBusquedaDTO
    {
        public bool Inactivo { get; set; }
        public List<string> TipoDeLugars { get; set; } = new List<string>();
        public string Ciudad { get; set; }
        public string MontoReservaMayorAMenor { get; set; }
        public List<string> TipoDeVehiculos { get; set; } = new List<string>();
    }
}
