using torneo.Models;
using System.Collections.Generic;
using System.Linq;

namespace torneo.Repositorio
{
    public class JugadorRepositorio
    {
        private List<Jugador> jugadores = new List<Jugador>();

        public void Agregar(Jugador jugador)
        {
            jugador.Id = jugadores.Count + 1;
            jugadores.Add(jugador);
        }

        public List<Jugador> ObtenerTodos()
        {
            return jugadores;
        }

        public Jugador? BuscarPorId(int id)
        {
            return jugadores.FirstOrDefault(j => j.Id == id);
        }

        public void Actualizar(int id, Jugador jugadorActualizado)
        {
            var jugador = BuscarPorId(id);
            if (jugador != null)
            {
                jugador.Nombre = jugadorActualizado.Nombre;
                jugador.Edad = jugadorActualizado.Edad;
                jugador.Posicion = jugadorActualizado.Posicion;
            }
        }

        public void Eliminar(int id)
        {
            var jugador = BuscarPorId(id);
            if (jugador != null)
                jugadores.Remove(jugador);
        }
    }
}