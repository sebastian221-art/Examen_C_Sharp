using torneo.Models;

namespace torneo.Repositorio
{
    public class TorneoRepositorio
    {
        private List<Torneo> Torneos = new List<Torneo>();

        public void Agregar(Torneo torneo)
        {
            torneo.Id = Torneos.Count + 1;
            Torneos.Add(torneo);
        }

        public List<Torneo> ObtenerTodos()
        {
            return Torneos;
        }

        public Torneo BuscarPorId(int id)
        {
            return Torneos.First(t => t.Id == id);
        }

        public void Eliminar(int id)
        {
            var torneo = BuscarPorId(id);
            Torneos.Remove(torneo);
        }

        public void Actualizar(int id, Torneo torneoActualizado)
        {
            var torneo = BuscarPorId(id);
            torneo.Nombre = torneoActualizado.Nombre;
            torneo.FechaInicio = torneoActualizado.FechaInicio;
            torneo.FechaFin = torneoActualizado.FechaFin;
        }
    }
}

