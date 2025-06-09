using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6NF2Prac.models;
using cat.itb.M6UF2EA3.connections;
using NHibernate;
using Npgsql;
using UF2_test.connections;

namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class ClientCRUD
    {
        public IList<Client> SelectAll()
        {
            IList<Client> clies;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                clies = (from e in session.Query<Client>() select e).ToList();
                session.Close();
            }
            return clies;
        }

        public IList<Client> SelectAllNadiu()
        {
            IList<Client> clients;

            using (var session = SessionFactoryHR2Cloud.Open())
            {
                IQuery query = session.CreateSQLQuery("SELECT * FROM client")
                                      .AddEntity(typeof(Client));

                clients = query.List<Client>();
            }

            return clients;
        }

        public Client SelectById(int id)
        {
            Client client;
            var session = SessionFactoryHR2Cloud.Open();
            client = session.Get<Client>(id);
            session.Close();
            return client;
        }

        public void Insert(Client client)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(client);
                    tx.Commit();
                    Console.WriteLine("Client {0} inserted", client.Name);
                    session.Close();
                }
            }
        }

        public void Update(Client client)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(client);
                tx.Commit();
                Console.WriteLine("Client {0} actualitzat", client.Name);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant client : " + ex.Message);
            }

            session.Close();
        }

        public void Delete(Client client)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(client);
                tx.Commit();
                Console.WriteLine("Client {0} esborrat", client.Name);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant client : " + ex.Message);
            }

            session.Close();
        }

        public Client SelectByName(string name)
        {
            Client clie;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery($"select c from Client c left join fetch c.Orders where c.Name = '{name}'");
            clie = query.UniqueResult<Client>();
            session.Close();

            return clie;
        }

        public IList<Client> SelectByCreditHigherThan(decimal credit)
        {
            IList<Client> clies;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                clies = (from e in session.Query<Client>() select e)
                    .Where(c => c.Credit > credit)
                    .ToList();
                session.Close();
            }
            return clies;
        }


        // ADO
        public void InsertADO(List<Client> clies)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            foreach (var clie in clies)
            {
                var sql = $"INSERT INTO client (code, name, credit) VALUES ({clie.Code}, '{clie.Name}', {clie.Credit})";

                var cmd = new NpgsqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Client with client name {0} added", clie.Name);
                }
                catch
                {
                    Console.WriteLine("Couldn't add client with name {0}", clie.Name);
                }
            }
            conn.Close();
        }

        public Client SelectByNameADO(string name)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM client WHERE name = @clieName", conn);
            cmd.Parameters.AddWithValue("clieName", name);
            cmd.Prepare();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Client clie = new Client();

            if (dr.Read())
            {
                clie.Id = dr.GetInt32(0);
                clie.Code = dr.GetInt32(1);
                clie.Name = dr.GetString(2);
                clie.Credit = dr.GetDecimal(3);
            }
            else
            {
                clie = null;
            }

            conn.Close();
            return clie;
        }

        public void DeleteADO(Client clie)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("DELETE FROM client WHERE id = @clieId", conn);
            cmd.Parameters.AddWithValue("clieId", clie.Id);
            cmd.Prepare();

            if (cmd.ExecuteNonQuery() != 0)
            {
                Console.WriteLine($"Client {clie.Name} deleted");
            }
            else
            {
                Console.WriteLine($"Client {clie.Name} doesn't exist");
            }
            conn.Close();
        }
    }
}
