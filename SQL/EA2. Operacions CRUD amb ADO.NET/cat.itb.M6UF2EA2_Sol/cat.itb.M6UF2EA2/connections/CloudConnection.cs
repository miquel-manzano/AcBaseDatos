using System;
using Npgsql;

namespace cat.itb.M6UF2EA2.connections
{
    /// <summary>
    /// 
    /// </summary>
    public class CloudConnection
    {
        private String HOST = "postgresql-miquel.alwaysdata.net"; // Ubicació de la BD.
        private String DB = "miquel_ea2"; // nom de la BD.
        private String USER = "miquel_admin";
        private String PASSWORD = "Sjo2025!";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}