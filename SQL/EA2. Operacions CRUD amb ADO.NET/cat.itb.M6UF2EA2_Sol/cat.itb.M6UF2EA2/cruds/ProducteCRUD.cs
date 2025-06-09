using System;
using System.Collections.Generic;
using cat.itb.M6UF2EA2.connections;
using cat.itb.M6UF2EA2.model;
using Npgsql;

namespace cat.itb.M6UF2EA2.cruds
{
    /// <summary>
    /// Classe per gestionar la taula producte
    /// </summary>
    public class ProducteCRUD
    {
        
        /// <summary>
        /// Mètode que retorna el producte que passes el id per paràmetre
        /// </summary>
        /// <returns>Un objecte Producte</returns>
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
        
        /// <summary>
        /// Mètode que inserta els productes especificats amb prepared statement
        /// </summary>
        /// <param name="productes">Llista d'objectes de tipus Producte</param>
        public void InsertIntoProducteWithPreparedStatement(List<Producte> productes)
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
        
        /// <summary>
        /// Mètode que elimina el prducte que passem el id per paràmetre
        /// </summary>
        /// <param name="id">Identificador del producte</param>
        /// <returns>True si s'ha eliminat i false si no es pot eliminar</returns>
        public Boolean Delete(int id)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            Boolean del = false;

            var cmd = new NpgsqlCommand("DELETE FROM producte WHERE prod_num = @prodNum", conn);
            cmd.Parameters.AddWithValue("prodNum", id);
            cmd.Prepare();

            if (cmd.ExecuteNonQuery() != 0)  del = true;
            conn.Close();
            return del;
        }
    }
}