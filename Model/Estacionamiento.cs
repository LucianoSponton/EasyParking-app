using System.Collections.Generic;

namespace Model
{
    public class Estacionamiento
    {

        public int Id { get; set; } // Id unico del estacionamiento
        public string UserId { get; set; } // Vinculacion de Id del dueño del estacionamiento
        public bool PublicacionPausada { get; set; } // Representa si la publicacion esta activa o inactiva
        public bool Inactivo { get; set; } // Representa baja logica del estacionamiento
        public byte[] Imagen { get; set; } // podria ser lista // imagen del lugar
        public string Ciudad { get; set; } // ciudad del lugar
        public string Nombre { get; set; } // nombre del lugar
        public string Direccion { get; set; } // Direccion del lugar
        public List<Jornada> Jornadas { get; set; } = new List<Jornada>(); // horarios en los que opera
        public string TipoDeLugar { get; set; } // descripcion del lugar - galpon, aire libre etc..
        public List<DataVehiculoAlojado> TiposDeVehiculosAdmitidos { get; set; } = new List<DataVehiculoAlojado>();// vehiculos aceptados
        public decimal MontoReserva { get; set; } // precio de la reserva si es que tiene 
        public string Observaciones { get; set; } // datos extra que el dueño quiera informar sobre el estacionamiento

    }

    //public class TipoDeLugar // Tablas -- podrian haber mas de los inicalmente planteados
    //{
    //    public int Id { get; set; }

    //    public string Descripcion { get; set; } // terreno aire abierto - etc..
    //}

    //public class TipoDeVehiculo // Tablas -- podrian haber mas de los inicalmente planteados
    //{
    //    public int Id { get; set; }

    //    public string Descripcion { get; set; } // auto - moto - camioneta
    //}
}
