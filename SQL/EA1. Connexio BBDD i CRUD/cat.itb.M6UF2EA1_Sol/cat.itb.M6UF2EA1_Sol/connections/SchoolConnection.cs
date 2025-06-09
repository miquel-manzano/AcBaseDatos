using Npgsql;

namespace cat.itb.M6UF2EA1_Sol.connections;

public class SchoolConnection
{
    private String HOST = "127.0.0.1:5432"; // Ubicació de la BD.
    private String DB = "school"; // Nom de la BD.
    private String USER = "school";
    private String PASSWORD = "school";

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
