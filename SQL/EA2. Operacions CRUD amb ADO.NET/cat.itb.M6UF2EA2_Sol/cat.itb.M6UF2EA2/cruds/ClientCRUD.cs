using System;
using System.Collections.Generic;
using cat.itb.M6UF2EA2.connections;
using cat.itb.M6UF2EA2.model;
using Npgsql;

namespace cat.itb.M6UF2EA2.cruds
{
    /// <summary>
    /// Classe per gestionar la taula client
    /// </summary>
    public class ClientCRUD
    {
       
        /// <summary>
        /// Mètode que retorna el client que passes el codi per paràmetre
        /// </summary>
        /// <param name="codi">Codi del Client</param>
        /// <returns>Un objecte client</returns>
        public Client Select(int codi)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM client WHERE client_cod = " + codi, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Client client = new Client();
            if (dr.Read())
            {
                client.clientCod = dr.GetInt32(0);
                client.nom = dr.GetString(1); 
                client.adreca = dr.GetString(2); 
                client.ciutat = dr.GetString(3); 
                client.estat = dr.GetString(4); 
                client.codiPostal = dr.GetString(5); 
                client.Area = dr.GetInt32(6);
                client.telefon = dr.GetString(7); 
                client.reprCod = dr.IsDBNull(8) ? (int?) null : dr.GetInt32(8);
                client.limitCredit = dr.GetDouble(9);
                client.observacions = dr.GetString(10); 
       
            }
            
            else
            {
                client = null;
               
            }
           
            conn.Close();
            return client;
        }
        
        
        /// <summary>
        /// Mètode que actualitza els clients especificats
        /// </summary>
        /// <param name="codis">Llista de ints que representen els codis de client</param>
        /// <param name="limits">Llista de doubles que representen els límits de credits</param>
        public void UpdateClientLimitCredit(List<int> codis,List<double> limits)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();
            var cmd = new NpgsqlCommand("UPDATE client SET limit_credit = @limitCredit WHERE client_cod = @clientCod", conn);
            
            for (int i = 0; i < 3; i++)
            {
                cmd.Parameters.AddWithValue("limitCredit", limits[i]);
                cmd.Parameters.AddWithValue("clientCod", codis[i]);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("limit_credit from client {0} updated to {1}", codis[i], limits[i]);
                }
                catch
                {
                    Console.WriteLine("Couldn't update credit limit for {0}", codis[i]);
                }
                
                cmd.Parameters.Clear();
            }
            
            conn.Close();
        }
        
        /// <summary>
        /// Mètode que elimina el client que passem el id per paràmetre
        /// </summary>
        /// <param name="codi">Codi del Client</param>
        /// <returns>True si s'ha eliminat i false si no es pot eliminar</returns>
        public Boolean Delete(int codi)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            Boolean del = false;

            var cmd = new NpgsqlCommand("DELETE FROM client WHERE client_cod = @clientCod", conn);
            cmd.Parameters.AddWithValue("clientCod", codi);
            cmd.Prepare();

            if (cmd.ExecuteNonQuery() != 0)  del = true;
            conn.Close();
            return del;
        }
    }
}