namespace torneo.Models
{
    public class CuerpoTecnico : Persona
    {
        public string? Rol { get; set; }
        public int EquipoId { get; set; }   // FK opcional: técnico asignado a un equipo
    }
}
