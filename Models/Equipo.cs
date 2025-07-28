namespace torneo.Models
{
    public class Equipo
    {
        public string? Nombre { get; set; }
        public int Id { get; set; }
        public List<Jugador> Jugadores { get; set; } = new List<Jugador>();
        public List<CuerpoTecnico> Tecnicos { get; set; } = new List<CuerpoTecnico>();
        public List<CuerpoMedico> Medicos { get; set; } = new List<CuerpoMedico>();

    }
}