using torneo.Data;
using torneo.Models;
using MySql.Data.MySqlClient;

namespace torneo.Repositorio
{
    public class JugadorRepositorio
    {
        public void Agregar(Jugador jugador)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = @"INSERT INTO Jugador 
                            (Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId) 
                            VALUES (@Nombre, @Edad, @Posicion, @Dorsal, @Asistencias, @ValorMercado, @EquipoId)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmd.Parameters.AddWithValue("@Edad", jugador.Edad);
            cmd.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmd.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            cmd.Parameters.AddWithValue("@Asistencias", jugador.Asistencias);
            cmd.Parameters.AddWithValue("@ValorMercado", jugador.ValorMercado);
            cmd.Parameters.AddWithValue("@EquipoId", jugador.EquipoId == 0 ? (object)DBNull.Value : jugador.EquipoId);

            cmd.ExecuteNonQuery();
        }

        public List<Jugador> ObtenerTodos()
        {
            List<Jugador> jugadores = new();

            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId FROM Jugador";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                jugadores.Add(new Jugador
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Edad = reader.GetInt32("Edad"),
                    Posicion = reader["Posicion"] as string,
                    Dorsal = reader.GetInt32("Dorsal"),
                    Asistencias = reader.GetInt32("Asistencias"),
                    ValorMercado = reader.GetDecimal("ValorMercado"),
                    EquipoId = reader["EquipoId"] == DBNull.Value ? 0 : reader.GetInt32("EquipoId")
                });
            }

            return jugadores;
        }

        public Jugador? BuscarPorId(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId FROM Jugador WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Jugador
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Edad = reader.GetInt32("Edad"),
                    Posicion = reader["Posicion"] as string,
                    Dorsal = reader.GetInt32("Dorsal"),
                    Asistencias = reader.GetInt32("Asistencias"),
                    ValorMercado = reader.GetDecimal("ValorMercado"),
                    EquipoId = reader["EquipoId"] == DBNull.Value ? 0 : reader.GetInt32("EquipoId")
                };
            }

            return null;
        }

        public void Actualizar(Jugador jugador)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = @"UPDATE Jugador 
                             SET Nombre=@Nombre, Edad=@Edad, Posicion=@Posicion, Dorsal=@Dorsal, 
                                 Asistencias=@Asistencias, ValorMercado=@ValorMercado, EquipoId=@EquipoId 
                             WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", jugador.Id);
            cmd.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmd.Parameters.AddWithValue("@Edad", jugador.Edad);
            cmd.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmd.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            cmd.Parameters.AddWithValue("@Asistencias", jugador.Asistencias);
            cmd.Parameters.AddWithValue("@ValorMercado", jugador.ValorMercado);
            cmd.Parameters.AddWithValue("@EquipoId", jugador.EquipoId == 0 ? (object)DBNull.Value : jugador.EquipoId);

            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "DELETE FROM Jugador WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public void TransferirJugador(int jugadorId, int nuevoEquipoId)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "UPDATE Jugador SET EquipoId=@NuevoEquipoId WHERE Id=@JugadorId";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NuevoEquipoId", nuevoEquipoId);
            cmd.Parameters.AddWithValue("@JugadorId", jugadorId);
            cmd.ExecuteNonQuery();
        }
    }
}
