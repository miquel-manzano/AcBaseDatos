using cat.itb.M6UF2EA1_Sol.connections;
using Npgsql;

namespace cat.itb.M6UF2EA1_Sol.cruds;

public class AlumnosCRUD
{
    public void SelectAll()
    {
        CloudConnection db = new CloudConnection();
       // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();
            
        NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM alumnos", conn);
        NpgsqlDataReader dr = cmd.ExecuteReader();
            
        while (dr.Read())
            Console.WriteLine("Dni: {0}, Cognoms i nom: {1}, Direcció: {2}, Població: {3}, Telèfon: {4}", 
                dr[0], dr[1], dr[2], dr[3], dr[4]);
            
        conn.Close();
    }
    
    public void insert(string pDNI, string pName, string pAddress, string pCity, string pPhone)
    {
        CloudConnection db = new CloudConnection();
       // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();
      
        var sql = "INSERT INTO alumnos VALUES " + "(" + "'" + pDNI + "'" +","+"'" +  pName + "'" + "," + "'" + pAddress + "'" + "," + "'" + pCity + "'" + "," + "'" + pPhone + "'" +")";
       using var cmd = new NpgsqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine("Inserted pupil " + pName);
    }
    
    public void UpdatePhone(string pDNI, string pPhone)
    {
        CloudConnection db = new CloudConnection();
       //SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();
        var sql = "UPDATE alumnos SET telef = " + "'" + pPhone + "'" + " WHERE dni = " + "'" + pDNI + "'"; 
        NpgsqlCommand cmd = new NpgsqlCommand(sql,conn);
        cmd.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine("Updated phone from pupil " +pDNI);
    }
    
    public void DeletePupilsFromCity(string pCity)
    {
        CloudConnection db = new CloudConnection();
       // SchoolConnection db = new SchoolConnection();
        var conn = db.GetConnection();
       // var sql = "DELETE FROM alumnos WHERE pobla = 'Mostoles'";
        var sql = "DELETE FROM alumnos WHERE pobla = " + "'" + pCity + "'"; 
        NpgsqlCommand cmd = new NpgsqlCommand(sql,conn);
        cmd.ExecuteNonQuery();
            
        conn.Close();
        Console.WriteLine("Deleted pupils from " + pCity);
    }
    
}