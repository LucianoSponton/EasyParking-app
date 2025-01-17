﻿using System.Collections.Generic;

namespace Model
{
    public class Jornada
    {
        public int Id { get; set; }
        public Estacionamiento Estacionamiento { get; set; }
        public Enums.Dia DiaDeLaSemana { get; set; }
        public List<RangoH> Horarios { get; set; } = new List<RangoH>();
    }
}
