using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6NF2Prac.models;
using cat.itb.M6UF2EA3.connections;
using Npgsql;
using UF2_test.connections;

namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class ProductCRUD
    {
        public IList<Product> SelectAll()
        {
            IList<Product> prods;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                prods = (from e in session.Query<Product>() select e).ToList();
                session.Close();
            }
            return prods;
        }

        public Product SelectById(int id)
        {
            Product prod;
            var session = SessionFactoryHR2Cloud.Open();
            prod = session.Get<Product>(id);
            session.Close();
            return prod;
        }

        public void Insert(Product prod)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(prod);
                    tx.Commit();
                    Console.WriteLine("Product {0} inserted", prod.Code);
                    session.Close();
                }
            }
        }

        public void Update(Product prod)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(prod);
                tx.Commit();
                Console.WriteLine("Product {0} actualitzat", prod.Code);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant product : " + ex.Message);
            }

            session.Close();
        }

        public void Delete(Product prod)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(prod);
                tx.Commit();
                Console.WriteLine("Product {0} esborrat", prod.Code);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant product : " + ex.Message);
            }

            session.Close();
        }

        public IList<object[]> SelectByPriceHigherThan(decimal price)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<object[]> productsData = session.QueryOver<Product>()
                .Where(c => c.Price > price)
                .SelectList(list => list
                    .Select(c => c.Description)
                    .Select(c => c.Price))
                .List<object[]>();
            session.Close();
            return productsData;
        }

        //ADO
        public Product SelectByCodeADO(int code)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM product WHERE code = @prodCode", conn);
            cmd.Parameters.AddWithValue("prodCode", code);
            cmd.Prepare();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Product prod = new Product();


            if (dr.Read())
            {
                var salespersonCRUD = new SalespersonCRUD();

                prod.Id = dr.GetInt32(0);
                prod.Code = dr.GetInt32(1);
                prod.Description = dr.GetString(2);
                prod.CurrentStock = dr.GetInt32(3);
                prod.MinStock = dr.GetInt32(4);
                prod.Price = dr.GetDecimal(5);
                prod.Salesperson = salespersonCRUD.SelectById(dr.GetInt32(6));
            }
            else
            {
                prod = null;
            }

            conn.Close();
            return prod;
        }

        public void UpdatePriceADO(Product newProd)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();
            var cmd = new NpgsqlCommand("UPDATE product SET price = @newPrice WHERE code = @prodCode", conn);


            cmd.Parameters.AddWithValue("newPrice", newProd.Price);
            cmd.Parameters.AddWithValue("prodCode", newProd.Code);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Price from product code: {newProd.Code} updated to {newProd.Price}");
            }
            catch
            {
                Console.WriteLine($"Couldn't update product with code: {newProd.Code}");
            }

            cmd.Parameters.Clear();

            conn.Close();
        }

        public List<Product> SelectByHighPriceADO(decimal price)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand($"SELECT * FROM product WHERE price > {price}", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Product> provs = new List<Product>();

            while (dr.Read())
            {
                var productCRUD = new ProductCRUD();

                Product prod = new Product();
                var salespersonCRUD = new SalespersonCRUD();

                prod.Id = dr.GetInt32(0);
                prod.Code = dr.GetInt32(1);
                prod.Description = dr.GetString(2);
                prod.CurrentStock = dr.GetInt32(3);
                prod.MinStock = dr.GetInt32(4);
                prod.Price = dr.GetDecimal(5);
                prod.Salesperson = salespersonCRUD.SelectById(dr.GetInt32(6));

                provs.Add(prod);
            }

            conn.Close();
            return provs;
        }

        public void InsertADO(Product prod)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO product (code, description, currentstock, minstock, price, salesp) VALUES (@code, @description, @currentstock, @minstock, @price, @salesp)", conn);

            
            cmd.Parameters.AddWithValue("code", prod.Code);
            cmd.Parameters.AddWithValue("description", prod.Description);
            cmd.Parameters.AddWithValue("currentstock", prod.CurrentStock);
            cmd.Parameters.AddWithValue("minstock", prod.MinStock);
            cmd.Parameters.AddWithValue("price", prod.Price);
            cmd.Parameters.AddWithValue("salesp", prod.Salesperson.Id);

            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Product with code {prod.Code} added");
            }
            catch
            {
                Console.WriteLine($"Couldn't add Product with code {prod.Code}");
            }

            cmd.Parameters.Clear();
            

            conn.Close();
        }

        public List<Product> SelectBySalesSurnameADO(string surname)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            var salesCRUD = new SalespersonCRUD();
            var salesperson = salesCRUD.SelectBySurenameADO(surname);

            var cmd = new NpgsqlCommand("SELECT * FROM product WHERE salesp = @salesId", conn);
            cmd.Parameters.AddWithValue("salesId", salesperson.Id);
            cmd.Prepare();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            
            List<Product> prods = new List<Product>();


            while (dr.Read())
            {
                var salespersonCRUD = new SalespersonCRUD();
                
                Product prod = new Product();
                prod.Id = dr.GetInt32(0);
                prod.Code = dr.GetInt32(1);
                prod.Description = dr.GetString(2);
                prod.CurrentStock = dr.GetInt32(3);
                prod.MinStock = dr.GetInt32(4);
                prod.Price = dr.GetDecimal(5);
                prod.Salesperson = salespersonCRUD.SelectById(dr.GetInt32(6));

                prods.Add(prod);
            }
            

            conn.Close();
            return prods;
        }
    }
}
