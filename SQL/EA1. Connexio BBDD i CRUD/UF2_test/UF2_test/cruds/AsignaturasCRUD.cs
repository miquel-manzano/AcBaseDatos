using Npgsql;

namespace UF2_test.cruds;

public class AsignaturasCRUD
{
    static NpgsqlConnection conn;

    public void SelectVersion()
    {
        //Preparo la connexió a la BBDD local
        SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
       // CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        var sql = "SELECT version()";

        using var cmd = new NpgsqlCommand(sql, conn);

        var version = cmd.ExecuteScalar().ToString();
        Console.WriteLine($"PostgreSQL version: {version}");
        
        // Close connection
        conn.Close();
    }

    public void SelectAllSubjects1()
    {
       
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
         CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        // Define a query
        NpgsqlCommand cmd = new NpgsqlCommand("select * from asignaturas", conn);

        // Execute a query
        NpgsqlDataReader dr = cmd.ExecuteReader();

        // Read all rows and output the first column in each row
        while (dr.Read())
          
            Console.Write("Codi: {0} "+ "Nom: {1} \n", dr[0], dr[1]);

        // Close connection
        conn.Close();
    }

    public void SelectAllSubjects2()
    {

        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        string sql = "SELECT * FROM asignaturas";
        
        using var cmd = new NpgsqlCommand(sql, conn);

        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
           Console.WriteLine("Codi: {0} "+ "Nom: {1} ", rdr.GetInt32(0), rdr.GetString(1));
        }
        // Close connection
        conn.Close();
    }

    public void SelectSubjectPS()
    {

        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        var sql = "SELECT * FROM asignaturas WHERE cod = @codi";
        
        using var cmd = new NpgsqlCommand(sql, conn);
        
        cmd.Parameters.AddWithValue("codi", 2);
        cmd.Prepare();

        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Console.WriteLine("Codi:{0} "+ "Nom:{1} ", rdr.GetInt32(0), rdr.GetString(1));
        }

      // Close connection
        conn.Close();
    }

    public void InsertSubject()
    {
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        using var cmd = new NpgsqlCommand();
        cmd.Connection = conn;
        
        cmd.CommandText = "INSERT INTO asignaturas(cod, nombre) VALUES(6,'BASES DE DADES')";
        cmd.ExecuteNonQuery();
        
        Console.WriteLine("row inserted");

        // Close connection
        conn.Close();
        
    }
    
    public void DeleteSubject(int codi)
    {
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        using var cmd = new NpgsqlCommand();
        cmd.Connection = conn;
        
        cmd.CommandText = "DELETE FROM asignaturas WHERE cod = " + codi;
        cmd.ExecuteNonQuery();
        
        Console.WriteLine("row deleted");

        // Close connection
        conn.Close();
    }
    
    public void InsertSubjectPS()
    {
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        var sql = "INSERT INTO asignaturas(cod,nombre) VALUES(@codi, @nom)";
        using var cmd = new NpgsqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("codi", 7);
        cmd.Parameters.AddWithValue("nom", "XARXES");
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        
        Console.WriteLine("row inserted");

        // Close connection
        conn.Close();
    }
    
    public void InsertMoreThanOneSubjectPS()
    {
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        //Creo la connexió i obtinc el NpgsqlConnection i el guardo a la variable conn
        var conn = db.GetConnection();
        
        //Preparo el Prepared Statement
        var sql = "INSERT INTO asignaturas(cod,nombre) VALUES(@codi, @nom)";
        //Creo un nou NpgsqlCommand amb el Prepared Statement  i la connexió 
        using var cmd = new NpgsqlCommand(sql, conn);

        //Assigno valor a les variable (paràmetre) @codi
        cmd.Parameters.AddWithValue("codi", 8);
        //Assigno valor a les variable (paràmetre)  @nom
        cmd.Parameters.AddWithValue("nom", "FILOSOFIA");
        //Preparo i executo el Prepared Statement
        cmd.Prepare();
        if (cmd.ExecuteNonQuery() != 0 )
        {
            //Informo a l'usuari si la inserció ha funcionat
            Console.WriteLine("row inserted");
        }
        else
        {
            //Informo a l'usuari si la inserció ha fallat
            Console.WriteLine("Inserted failed");      
        }
       
        //Netejo els paràmetres
        cmd.Parameters.Clear();
         
        //Torno a assignar un altre valor a les variable (paràmetre) @codi             
        cmd.Parameters.AddWithValue("codi", 9);                      
        //Torno a assignar un altre valor a les variable (paràmetre)  @nom             
        cmd.Parameters.AddWithValue("nom", "BIOLOGIA");             
        //Preparo i executo el Prepared Statement                    
        cmd.Prepare();                                               
        if (cmd.ExecuteNonQuery() != 0 )                             
        {                                                            
            //Informo a l'usuari si la inserció ha funcionat         
            Console.WriteLine("row inserted");                       
        }                                                            
        else                                                         
        {                                                            
            //Informo a l'usuari si la inserció ha fallat            
            Console.WriteLine("Inserted failed");                    
        }
        
//Netejo els paràmetres                                      
        cmd.Parameters.Clear();                                      
                                                             
// Close connection                                          
        conn.Close();                                                
    }
    
    public void DeleteSubjectPS(int codi)
    {
        //Preparo la connexió a la BBDD local
       // SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();
        
        var sql = "DELETE FROM asignaturas WHERE cod = @codi";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("codi", codi);
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        
        Console.WriteLine("row deleted");

        // Close connection
        conn.Close();        
    }

    public void UpdateSubjectPS(int pcodi, string pnom)
    {
        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();

        var sql = "UPDATE asignaturas SET nombre = @nom WHERE cod = @codi";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("codi", pcodi);
        cmd.Parameters.AddWithValue("nom", pnom);
        cmd.Prepare();
        cmd.ExecuteNonQuery();

        Console.WriteLine("row updated");

        // Close connection
        conn.Close();
    }
    public void SelectAllSubjects3()
    {

        //Preparo la connexió a la BBDD local
        //SchoolConnection db = new SchoolConnection();

        //Preparo la connexió al cloud 
        CloudConnection db = new CloudConnection();

        var conn = db.GetConnection();

        string sql = "SELECT * FROM asignaturas";

        using var cmd = new NpgsqlCommand(sql, conn);

        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10}");

        while (rdr.Read())
        {
            Console.WriteLine($"{rdr.GetInt32(0),-4} {rdr.GetString(1),-10}");
        }
        // Close connection
        conn.Close();
    }
}