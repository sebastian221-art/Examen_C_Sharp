namespace torneo.Models
{
    public class Jugador : Persona
    {
        public int Dorsal { get; set; }
        public int Edad { get; set; }
        public string? Posicion { get; set; }
        public int Asistencias { get; set; }
        public decimal ValorMercado { get; set; }
        public int EquipoId { get; set; }   // FK: jugador pertenece a un equipo
    }
}
