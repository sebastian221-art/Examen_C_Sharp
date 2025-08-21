namespace torneo.Models
{
    public class CuerpoMedico : Persona
    {
        public string? Especialidad { get; set; }
        public int EquipoId { get; set; }   // FK opcional: médico asignado a un equipo
    }
}
