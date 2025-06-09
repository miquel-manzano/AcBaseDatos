using Npgsql;

namespace UF2_test;

public class SchoolConnection
{
    private String HOST = "127.0.0.1:5432"; // Ubicació del servidor i el port de PotgreSQL.
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
