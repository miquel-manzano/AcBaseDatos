using cat.itb.M6UF2EA1_Sol.connections;
using Npgsql;

namespace cat.itb.M6UF2EA1_Sol.cruds;

public class NotasCRUD
{
    
    public void SelectAll()
    {
         CloudConnection db = new CloudConnection();
       // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();
            
        NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM notas", conn);
        NpgsqlDataReader dr = cmd.ExecuteReader();
            
        while (dr.Read())
            Console.WriteLine("Dni: {0}, Codi: {1}, Nota: {2}", 
                dr[0], dr[1], dr[2]);
            
        conn.Close();
    }
    
    public void SelectMarksFromPupil(string pDNI)
    {
        CloudConnection db = new CloudConnection();
      // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();

        NpgsqlCommand cmd = new NpgsqlCommand("SELECT nota FROM notas WHERE dni = @dni", conn);
        cmd.Parameters.AddWithValue("dni", pDNI);
        cmd.Prepare();
        NpgsqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
            Console.WriteLine("Nota:{0} ", dr[0]);
            
        conn.Close();
    }
    
    public void InsertMaks(List<string> dnis,List<int> codis, int pnota)
            {
                CloudConnection db = new CloudConnection();
               //SchoolConnection db = new SchoolConnection();
                var conn = db.GetConnection();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO notas VALUES(@dni, @cod, @nota)", conn);
                
               cmd.Parameters.AddWithValue("dni", dnis[0]);
               cmd.Parameters.AddWithValue("cod", codis[0]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               
               cmd.Parameters.AddWithValue("dni", dnis[0]);
               cmd.Parameters.AddWithValue("cod", codis[1]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               
               cmd.Parameters.AddWithValue("dni", dnis[1]);
               cmd.Parameters.AddWithValue("cod", codis[0]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               
               cmd.Parameters.AddWithValue("dni", dnis[1]);
               cmd.Parameters.AddWithValue("cod", codis[1]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               
               
               cmd.Parameters.AddWithValue("dni", dnis[2]);
               cmd.Parameters.AddWithValue("cod", codis[0]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               
               cmd.Parameters.AddWithValue("dni", dnis[2]);
               cmd.Parameters.AddWithValue("cod", codis[1]);
               cmd.Parameters.AddWithValue("nota", pnota);
               cmd.Prepare();
               cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
    
               conn.Close();
               Console.WriteLine("Inserted marks");
            
            }

    public void UpdateMarks(string pName, List<int> codis, int pNota)
    {
        CloudConnection db = new CloudConnection();
        // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();

        var sql = "UPDATE notas SET nota = " + "'" + pNota + "'" + "WHERE dni = (SELECT dni FROM alumnos WHERE apenom =" + "'" + pName + "'" + ")" + "AND (cod = " + "'" + codis[0] + "'" + "OR cod =" + "'" + codis[1] + "'" + ")";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
            
        conn.Close();
        Console.WriteLine("Updated marks");
    }

    public void UpdateMarks2(string pName, List<string> codis, int pNota)
    {
        CloudConnection db = new CloudConnection();
        // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();

        //var sql = "UPDATE notas SET nota = " + "'"+ pNota + "'" + "WHERE dni = (SELECT dni FROM alumnos WHERE apenom =" + "'" + pName + "'" + ")" + "AND (cod = " + "'" + codis[0] + "'" + "OR cod =" + "'" + codis[1] + "'" + ")";
        var sql = "UPDATE notas SET nota = " + "'" + pNota + "'" + "WHERE dni = (SELECT dni FROM alumnos WHERE apenom =" + "'" + pName + "'" + ")" + "AND (cod = " + "(SELECT cod FROM asignaturas WHERE nombre =" + "'" + codis[0] + "'" + ")" + "OR cod =" + "(SELECT cod FROM asignaturas WHERE nombre =" + "'" + codis[1] + "'" + ")" + ")";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.ExecuteNonQuery();

        conn.Close();
        Console.WriteLine("Updated marks");
    }
}