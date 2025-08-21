using System;
using System.Collections.Generic;

namespace torneo.Models
{
    public class Torneo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public List<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}
