using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using UF2_test.model;

namespace UF2_test.cruds
{
    public class ProducteCRUD
    {
        // PS: utilitzant el PreparedStatement

        public Producte Select(int id)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM producte WHERE prod_num = @prodNum", conn);
            cmd.Parameters.AddWithValue("prodNum", id);
            cmd.Prepare();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Producte prod = new Producte();

            if (dr.Read())
            {
                prod.prodNum = dr.GetInt32(0);
                prod.descripcio = dr.GetString(1);
            }
            else
            {
                prod = null;
            }

            conn.Close();
            return prod;
        }

        public void InsertPS(List<Producte> productes)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO producte VALUES (@prodNum, @descripcio)", conn);

            foreach (var producte in productes)
            {
                cmd.Parameters.AddWithValue("prodNum", producte.prodNum);
                cmd.Parameters.AddWithValue("descripcio", producte.descripcio);
                cmd.Prepare();

                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Producte with prodNum {0} and descripcio {1} added",
                        producte.prodNum, producte.descripcio);
                }
                catch
                {
                    Console.WriteLine("Couldn't add Producte with prodNum {0}", producte.prodNum);
                }

                cmd.Parameters.Clear();
            }

            conn.Close();
        }

        public void DeletePS(int id)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("DELETE FROM producte WHERE prod_num = @prodCod", conn);
            cmd.Parameters.AddWithValue("prodCod", id);
            cmd.Prepare();

            if (cmd.ExecuteNonQuery() != 0)
            {
                Console.WriteLine($"Producte {id} deleted");
            }
            else
            {
                Console.WriteLine($"Producte {id} doesn't exist");
            }
            conn.Close();
        }
    }
}
