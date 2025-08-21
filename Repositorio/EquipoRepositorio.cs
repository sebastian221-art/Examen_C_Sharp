using torneo.Data;
using torneo.Models;
using MySql.Data.MySqlClient;

namespace torneo.Repositorio
{
    public class EquipoRepositorio
    {
        public void Agregar(Equipo equipo)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "INSERT INTO Equipo (Nombre, GolesContra) VALUES (@Nombre, @GolesContra)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", equipo.Nombre);
            cmd.Parameters.AddWithValue("@GolesContra", equipo.GolesContra);

            cmd.ExecuteNonQuery();
        }

        public List<Equipo> ObtenerTodos()
        {
            List<Equipo> equipos = new();

            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, GolesContra FROM Equipo";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                equipos.Add(new Equipo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    GolesContra = reader.GetInt32("GolesContra")
                });
            }

            return equipos;
        }

        public Equipo? BuscarPorId(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "SELECT Id, Nombre, GolesContra FROM Equipo WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Equipo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    GolesContra = reader.GetInt32("GolesContra")
                };
            }

            return null;
        }

        public void Actualizar(Equipo equipo)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "UPDATE Equipo SET Nombre=@Nombre, GolesContra=@GolesContra WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", equipo.Id);
            cmd.Parameters.AddWithValue("@Nombre", equipo.Nombre);
            cmd.Parameters.AddWithValue("@GolesContra", equipo.GolesContra);

            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = "DELETE FROM Equipo WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
