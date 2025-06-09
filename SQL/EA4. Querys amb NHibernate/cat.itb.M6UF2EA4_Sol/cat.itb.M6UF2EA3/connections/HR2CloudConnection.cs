using System;
using Npgsql;

namespace cat.itb.M6UF2EA3.connections
{
    public class HR2CloudConnection
    {
        private String HOST = "postgresql-joancolomer.alwaysdata.net:5432"; // Ubicació de la BD.
        private String DB = "joancolomer_hr2"; // nom de la BD.
        private String USER = "joancolomer";
        private String PASSWORD = "itbPassITB";

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