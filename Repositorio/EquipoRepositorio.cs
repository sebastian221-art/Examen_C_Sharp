using System.Collections.Generic;
using torneo.Models;
using System.Linq;

namespace torneo.Repositorio
{
    public class EquipoRepositorio
    {
        private List<Equipo> Equipos = new List<Equipo>();

        public void Agregar(Equipo equipo)
        {
            equipo.Id = Equipos.Count + 1;
            Equipos.Add(equipo);
        }

        public List<Equipo> ObtenerTodos()
        {
            return Equipos;
        }

        public Equipo BuscarPorId(int id)
        {
            return Equipos.First(e => e.Id == id);
        }
    }
}