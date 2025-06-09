using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using UF2_test.model;

namespace UF2_test.cruds
{
    public class ClientCRUD
    {
        // PS: utilitzant el PreparedStatement

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
                client.reprCod = dr.IsDBNull(8) ? (int?)null : dr.GetInt32(8);
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

        public void UpdateClientLimitCreditPS(int codi, double limit)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();
            var cmd = new NpgsqlCommand("UPDATE client SET limit_credit = @limitCredit WHERE client_cod = @clientCod", conn);

            
            cmd.Parameters.AddWithValue("limitCredit", limit);
            cmd.Parameters.AddWithValue("clientCod", codi);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("limit_credit from client {0} updated to {1}", codi, limit);
            }
            catch
            {
                Console.WriteLine("Couldn't update credit limit for {0}", codi);
            }

            cmd.Parameters.Clear();

            conn.Close();
        }

        public void DeletePS(int codi)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("DELETE FROM client WHERE client_cod = @clientCod", conn);
            cmd.Parameters.AddWithValue("clientCod", codi);
            cmd.Prepare();

            if (cmd.ExecuteNonQuery() != 0)
            {
                Console.WriteLine($"Client {codi} deleted");
            }
            else
            {
                Console.WriteLine($"Client {codi} doesn't exist");
            }
                conn.Close();
        }
    }
}
