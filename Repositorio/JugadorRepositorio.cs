using System.Collections.Generic;
using torneo.Models;
using System.Linq;

namespace torneo.Repositorio
{
    public class JugadorRepositorio
    {
        public List<Jugador> Jugadores = new List<Jugador>();

        public void Agregar(Jugador jugador)
        {
            jugador.Id = Jugadores.Count + 1;
            Jugadores.Add(jugador);
        }

        public List<Jugador> ObtenerTodos()
        {
            return Jugadores;
        }

        public Jugador BuscarPorId(int id)
        {
            return Jugadores.First(j => j.Id == id);
        }
    }
}