using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6NF2Prac.models;
using cat.itb.M6UF2EA3.connections;
using NHibernate;
using NHibernate.Criterion;
using Npgsql;
using UF2_test.connections;

namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class ProviderCRUD
    {
        public IList<Provider> SelectAll()
        {
            IList<Provider> providers;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                providers = (from e in session.Query<Provider>() select e).ToList();
                session.Close();
            }
            return providers;
        }

        public Provider SelectById(int id)
        {
            Provider provider;
            var session = SessionFactoryHR2Cloud.Open();
            provider = session.Get<Provider>(id);
            session.Close();
            return provider;
        }

        public void Insert(Provider provider)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(provider);
                    tx.Commit();
                    Console.WriteLine("Provider {0} inserted", provider.Name);
                    session.Close();
                }
            }
        }

        public void Update(Provider provider)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(provider);
                tx.Commit();
                Console.WriteLine("Provider {0} actualitzat", provider.Name);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant provider : " + ex.Message);
            }

            session.Close();
        }

        public void Delete(Provider provider)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(provider);
                tx.Commit();
                Console.WriteLine("Provider {0} esborrat", provider.Name);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant crovider : " + ex.Message);
            }

            session.Close();
        }

        public Provider SelectLowestAmount()
        {
            var session = SessionFactoryHR2Cloud.Open();
            QueryOver<Provider> lowestAmount = QueryOver.Of<Provider>()
                .SelectList(p => p.SelectMin(c => c.Amount));
            Provider provider = session.QueryOver<Provider>()
                .WithSubquery.WhereProperty(c => c.Amount)
                .Eq(lowestAmount).SingleOrDefault();
            return provider;
        }

        public IList<Provider> SelectByCity(string city)
        {
            IList<Provider> providers;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery($"select c from Provider c where c.City like '{city}'");
            providers = query.List<Provider>();
            session.Close();

            return providers;
        }

        // ADO
        public List<Provider> SelectCreditLowerThanADO(decimal credit)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand($"SELECT * FROM provider WHERE credit < {credit}", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Provider> provs = new List<Provider>();

            while (dr.Read())
            {
                var productCRUD = new ProductCRUD();

                Provider prov = new Provider();
                prov.Id = dr.GetInt32(0);
                prov.Name = dr.GetString(1);
                prov.Address = dr.GetString(2);
                prov.City = dr.GetString(3);
                prov.StateCode = dr.GetString(4);
                prov.ZipCode = dr.GetString(5);
                prov.Area = dr.GetInt32(6);
                prov.Phone = dr.GetString(7);
                prov.Product = productCRUD.SelectById(dr.GetInt32(8));
                prov.Amount = dr.GetInt32(9);
                prov.Credit = dr.GetDecimal(10);
                prov.Remark = dr.GetString(11);

                provs.Add(prov);
            }

            conn.Close();
            return provs;
        }

        public List<Provider> SelectByCityADO(string city)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand($"SELECT * FROM provider WHERE city = '{city}'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Provider> providers = new List<Provider>();

            while (dr.Read())
            {
                var productCRUD = new ProductCRUD();

                Provider prov = new Provider();
                prov.Id = dr.GetInt32(0);
                prov.Name = dr.GetString(1);
                prov.Address = dr.GetString(2);
                prov.City = dr.GetString(3);
                prov.StateCode = dr.GetString(4);
                prov.ZipCode = dr.GetString(5);
                prov.Area = dr.GetInt32(6);
                prov.Phone = dr.GetString(7);
                prov.Product = productCRUD.SelectById(dr.GetInt32(8));
                prov.Amount = dr.GetInt32(9);
                prov.Credit = dr.GetDecimal(10);
                prov.Remark = dr.GetString(11);

                providers.Add(prov);
            }

            conn.Close();
            return providers;
        }

        public void UpdateADO(Provider prov)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();
            var cmd = new NpgsqlCommand("UPDATE provider SET name = @name, address = @address, city = @city, stcode = @stcode, zipcode = @zipcode, area = @area, phone = @phone, product = @product, amount = @amount, credit = @credit, remark = @remark WHERE id = @id", conn);


            cmd.Parameters.AddWithValue("id", prov.Id);

            cmd.Parameters.AddWithValue("name", prov.Name);
            cmd.Parameters.AddWithValue("address", prov.Address);
            cmd.Parameters.AddWithValue("city", prov.City);
            cmd.Parameters.AddWithValue("stcode", prov.StateCode);
            cmd.Parameters.AddWithValue("zipcode", prov.ZipCode);
            cmd.Parameters.AddWithValue("area", prov.Area);
            cmd.Parameters.AddWithValue("phone", prov.Phone);
            cmd.Parameters.AddWithValue("product", prov.Product.Id);
            cmd.Parameters.AddWithValue("amount", prov.Amount);
            cmd.Parameters.AddWithValue("credit", prov.Credit);
            cmd.Parameters.AddWithValue("remark", prov.Remark);

            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Provider with id {prov.Id} updated");
            }
            catch
            {
                Console.WriteLine($"Couldn't update Provider with id {prov.Id}");
            }

            cmd.Parameters.Clear();

            conn.Close();
        }

        public void InserADO(Provider prov)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO provider (name, address, city, stcode, zipcode, area, phone, product, amount, credit, remark) VALUES (@name, @address, @city, @stcode, @zipcode, @area, @phone, @product, @amount, @credit, @remark)", conn);


            cmd.Parameters.AddWithValue("name", prov.Name);
            cmd.Parameters.AddWithValue("address", prov.Address);
            cmd.Parameters.AddWithValue("city", prov.City);
            cmd.Parameters.AddWithValue("stcode", prov.StateCode);
            cmd.Parameters.AddWithValue("zipcode", prov.ZipCode);
            cmd.Parameters.AddWithValue("area", prov.Area);
            cmd.Parameters.AddWithValue("phone", prov.Phone);
            cmd.Parameters.AddWithValue("product", prov.Product.Id);
            cmd.Parameters.AddWithValue("amount", prov.Amount);
            cmd.Parameters.AddWithValue("credit", prov.Credit);
            cmd.Parameters.AddWithValue("remark", prov.Remark);

            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Provider with name {prov.Name} added");
            }
            catch
            {
                Console.WriteLine($"Couldn't add Provider with name {prov.Name}");
            }

            cmd.Parameters.Clear();


            conn.Close();
        }
    }
}
