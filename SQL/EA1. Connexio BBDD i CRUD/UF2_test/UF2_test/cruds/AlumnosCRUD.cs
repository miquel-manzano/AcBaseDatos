using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace UF2_test.cruds
{
    public class AlumnosCRUD
    {
        public void SelectAll()
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM alumnos", conn);

            NpgsqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine("DNI: {0}, Nom: {1}, Direcció: {2}, Poblacio: {3}, Telefon: {4}",
                    rd[0], rd[1], rd[2], rd[3], rd[4]);
            }
            conn.Close();
        }

        public void SelectByDNI(string dni)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var sql = "SELECT * FROM alumnos WHERE dni = @dni";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("dni", dni);
            cmd.Prepare();

            using NpgsqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Console.WriteLine("DNI: {0}, Nom: {1}, Direcció: {2}, Poblacio: {3}, Telefon: {4}",
                    rd[0], rd[1], rd[2], rd[3], rd[4]);
            }

            conn.Close();
        }

        public void Insert(string aDNI, string aName, string aAddress, string aCity, string aPhone)
        {
            CloudConnection db = new CloudConnection();
            // SchoolConnection db = new SchoolConnection();
            var conn = db.GetConnection();

            var sql = $"INSERT INTO alumnos VALUES ('{aDNI}','{aName}','{aAddress}','{aCity}','{aPhone}')";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Inserted alumne " + aName);
        }

        public void UpdatePhone(string aDNI, string aPhone)
        {
            CloudConnection db = new CloudConnection();

            var conn = db.GetConnection();
            var sql = $"UPDATE alumnos SET telef = '{aPhone}' WHERE dni = '{aDNI}'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Updated phone from alumne " + aDNI);
        }

        public void DeletePupilsFromCity(string pCity)
        {
            CloudConnection db = new CloudConnection();
            
            var conn = db.GetConnection();
            var sql = $"DELETE FROM alumnos WHERE pobla = '{pCity}'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine("Deleted pupils from " + pCity);
        }
    }
}
