using torneo.Data;
using torneo.Models;
using MySql.Data.MySqlClient;

namespace torneo.Repositorio
{
    public class TorneoRepositorio
    {
        public void Agregar(Torneo torneo)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "INSERT INTO Torneo (Nombre, FechaInicio, FechaFin) VALUES (@Nombre, @FechaInicio, @FechaFin)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", torneo.Nombre);
            cmd.Parameters.AddWithValue("@FechaInicio", torneo.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", torneo.FechaFin);

            cmd.ExecuteNonQuery();
        }

        public List<Torneo> ObtenerTodos()
        {
            List<Torneo> torneos = new();

            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, FechaInicio, FechaFin FROM Torneo";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                torneos.Add(new Torneo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    FechaInicio = reader.GetDateTime("FechaInicio"),
                    FechaFin = reader.GetDateTime("FechaFin")
                });
            }

            return torneos;
        }

        public Torneo? BuscarPorId(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, FechaInicio, FechaFin FROM Torneo WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Torneo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    FechaInicio = reader.GetDateTime("FechaInicio"),
                    FechaFin = reader.GetDateTime("FechaFin")
                };
            }

            return null;
        }

        public void Actualizar(Torneo torneo)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "UPDATE Torneo SET Nombre=@Nombre, FechaInicio=@FechaInicio, FechaFin=@FechaFin WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", torneo.Id);
            cmd.Parameters.AddWithValue("@Nombre", torneo.Nombre);
            cmd.Parameters.AddWithValue("@FechaInicio", torneo.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", torneo.FechaFin);

            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "DELETE FROM Torneo WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
