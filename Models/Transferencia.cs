namespace torneo.Models
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int JugadorId { get; set; }
        public int? EquipoOrigenId { get; set; }
        public int? EquipoDestinoId { get; set; }
        public DateTime FechaTransferencia { get; set; } = DateTime.Today;
        public decimal Precio { get; set; }

        public string? NombreJugador { get; set; }
        public string? NombreEquipoOrigen { get; set; }
        public string? NombreEquipoDestino { get; set; }
    }
}
