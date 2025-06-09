using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace UF2_test.cruds
{
    public class NotasCRUD
    {
        public void SelectAll()
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM notas", conn);

            NpgsqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine("DNI: {0}, Codi: {1}, Nota: {2}",
                    rd[0], rd[1], rd[2]);
            }
            conn.Close();
        }

        public void SelectByDNI(string dni)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var sql = "SELECT * FROM notas WHERE dni = @dni";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("dni", dni);
            cmd.Prepare();

            using NpgsqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Console.WriteLine("DNI: {0}, Codi: {1}, Nota: {2}",
                    rd[0], rd[1], rd[2]);
            }

            conn.Close();
        }

        public void Insert(string aDNI, int aCod, int aNota)
        {
            CloudConnection db = new CloudConnection();
            
            var conn = db.GetConnection();
            using var cmd = new NpgsqlCommand("INSERT INTO alumnos VALUES (@dni, @cod, @nota)", conn);
            
            cmd.Parameters.AddWithValue("dni", aDNI);
            cmd.Parameters.AddWithValue("cod", aCod);
            cmd.Parameters.AddWithValue("nota", aNota);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            conn.Close();

            Console.WriteLine("Inserted nota from alumne: " + aDNI);
        }

        public void Update2(string aName, List<string> codis, int nota)
        {
            CloudConnection db = new CloudConnection();
            
            var conn = db.GetConnection();

            var sql = $"UPDATE notas SET nota = '{nota}' WHERE dni = (SELECT dni FROM alumnos WHERE apenom ='{aName}') AND (cod = (SELECT cod FROM asignaturas WHERE nombre ='{codis[0]}') OR cod =(SELECT cod FROM asignaturas WHERE nombre ='{codis[1]}'))";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine("Updated mark");
        }
    }
}
