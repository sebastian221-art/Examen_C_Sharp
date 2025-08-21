using System.Collections.Generic;

namespace torneo.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int TorneoId { get; set; }   // FK: equipo pertenece a un torneo
        public int GolesContra { get; set; }

        public List<Jugador> Jugadores { get; set; } = new List<Jugador>();
        public List<CuerpoTecnico> Tecnicos { get; set; } = new List<CuerpoTecnico>();
        public List<CuerpoMedico> Medicos { get; set; } = new List<CuerpoMedico>();
    }
}
