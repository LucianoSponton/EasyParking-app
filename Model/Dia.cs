using System.Collections.Generic;

namespace Model
{
    public class Dia
    {
        public Enums.Dia DiaDeLaSemana { get; set; }
        public List<RangoH> Horarios { get; set; } = new List<RangoH>();
    }
}
