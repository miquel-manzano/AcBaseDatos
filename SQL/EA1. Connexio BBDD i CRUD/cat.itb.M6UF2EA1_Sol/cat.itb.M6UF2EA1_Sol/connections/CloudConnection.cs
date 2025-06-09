using Npgsql;

namespace cat.itb.M6UF2EA1_Sol.connections;


public class CloudConnection
{
    private String HOST = "postgresql-miquel.alwaysdata.net"; // Ubicació de la BD.
    private String DB = "miquel_ea1"; // Nom de la BD.
    private String USER = "miquel_admin";
    private String PASSWORD = "Sjo2025!";

    /**
      * Mètode per connectar al cloud ElephantSQL
      */
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