using Npgsql;

namespace UF2_test;

public class CloudConnection
{
    private String HOST = "postgresql-miquel.alwaysdata.net"; // Ubicació de la BD.
    private String DB = "miquel_ea2"; // nom de la BD.
    private String USER = "miquel_admin";
    private String PASSWORD = "Sjo2025!";

    // Specify connection options and open an connection
    public NpgsqlConnection conn = null;

    /**
     * Mètode per connectar a la base de dades school
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