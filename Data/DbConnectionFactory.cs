using MySql.Data.MySqlClient;

namespace torneo.Data
{
    public static class DbConnectionFactory
    {
        // 📌 Ajusta los datos de conexión según tu MySQL
        private static readonly string connectionString = 
            "Server=localhost;Database=LeagueMasterDB;User=campus2023;Password=campus2023;";

        /// <summary>
        /// Crea y devuelve una conexión a MySQL.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
