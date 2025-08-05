

using torneo.Models;
using System.Collections.Generic;
using System.Linq;

namespace torneo.Repositorio
{
    public class EquipoRepositorio
    {
        private List<Equipo> equipos = new List<Equipo>();

        public void Agregar(Equipo equipo)
        {
            equipo.Id = equipos.Count + 1;
            equipos.Add(equipo);
        }

        public List<Equipo> ObtenerTodos()
        {
            return equipos;
        }

        public Equipo? BuscarPorId(int id)
        {
            return equipos.FirstOrDefault(e => e.Id == id);
        }

        public void Eliminar(int id)
        {
            var equipo = BuscarPorId(id);
            if (equipo != null)
                equipos.Remove(equipo);
        }

        public void Actualizar(int id, Equipo equipoActualizado)
        {
            var equipo = BuscarPorId(id);
            if (equipo != null)
            {
                equipo.Nombre = equipoActualizado.Nombre;
            }
        }
    }
}
