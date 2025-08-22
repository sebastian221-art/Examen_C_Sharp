using torneo.Models;
using torneo.Data;
using MySql.Data.MySqlClient;

namespace torneo.Repositorio
{
    public class TransferenciaRepositorio
    {
        public void RegistrarTransferencia(Transferencia transferencia)
        {
            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = @"INSERT INTO Transferencia 
                            (JugadorId, EquipoOrigenId, EquipoDestinoId, FechaTransferencia, Precio) 
                            VALUES (@JugadorId, @EquipoOrigenId, @EquipoDestinoId, @Fecha, @Precio)";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@JugadorId", transferencia.JugadorId);
            cmd.Parameters.AddWithValue("@EquipoOrigenId", transferencia.EquipoOrigenId == 0 ? (object)DBNull.Value : transferencia.EquipoOrigenId);
            cmd.Parameters.AddWithValue("@EquipoDestinoId", transferencia.EquipoDestinoId == 0 ? (object)DBNull.Value : transferencia.EquipoDestinoId);
            cmd.Parameters.AddWithValue("@Fecha", transferencia.FechaTransferencia);
            cmd.Parameters.AddWithValue("@Precio", transferencia.Precio);

            cmd.ExecuteNonQuery();
        }

        public List<Transferencia> ObtenerHistorial()
        {
            List<Transferencia> lista = new();

            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            string query = @"SELECT t.Id, t.JugadorId, j.Nombre AS NombreJugador,
                                    t.EquipoOrigenId, eo.Nombre AS NombreEquipoOrigen,
                                    t.EquipoDestinoId, ed.Nombre AS NombreEquipoDestino,
                                    t.FechaTransferencia, t.Precio
                             FROM Transferencia t
                             LEFT JOIN Jugador j ON t.JugadorId = j.Id
                             LEFT JOIN Equipo eo ON t.EquipoOrigenId = eo.Id
                             LEFT JOIN Equipo ed ON t.EquipoDestinoId = ed.Id
                             ORDER BY t.FechaTransferencia DESC";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Transferencia
                {
                    Id = reader.GetInt32("Id"),
                    JugadorId = reader.GetInt32("JugadorId"),
                    NombreJugador = reader["NombreJugador"] as string,
                    EquipoOrigenId = reader["EquipoOrigenId"] == DBNull.Value ? null : reader.GetInt32("EquipoOrigenId"),
                    EquipoDestinoId = reader["EquipoDestinoId"] == DBNull.Value ? null : reader.GetInt32("EquipoDestinoId"),
                    NombreEquipoOrigen = reader["NombreEquipoOrigen"] as string,
                    NombreEquipoDestino = reader["NombreEquipoDestino"] as string,
                    FechaTransferencia = reader.GetDateTime("FechaTransferencia"),
                    Precio = reader.GetDecimal("Precio")
                });
            }

            return lista;
        }
    }
}
