using cat.itb.M6UF3EA3_Sol.connections;
using cat.itb.M6UF3EA3_Sol.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA3_Sol.cruds
{
    public class ProductsCRUD
    {
        public void LoadProductsCollection()
        {
            FileInfo file = new FileInfo("../../../files/products.json");
            var database = MongoLocalConnection.GetDatabase("itb");

            database.DropCollection("products");

            var collection = database.GetCollection<Product>("products");

            using (StreamReader sr = file.OpenText())
            {
                string productString;
                while ((productString = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(productString);
                    collection.InsertOne(product);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCollection products loaded");
            Console.ResetColor();
        }

        public void SelectMoreExpensiveProduct()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Product>("products");

            var query = booksCollection.AsQueryable<Product>();

            var maxPrice =
                query
                    .Select(p => p.Price)
                    .Max();
            var product =
                query
                    .Where(p => p.Price == maxPrice)
                    .Single();

            Console.WriteLine("El producte més car és:{0} " + "Preu :{1} ", product.Name, product.Price);

        }

        public void SelectChipestProduct2()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Product>("products");

            var product = collection.AsQueryable()
                .OrderBy(p => p.Price).First();
            Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price);
        }

        public void SelectTotalStock()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Product>("products");

            var query = booksCollection.AsQueryable<Product>();

            var totalStock =
                query
                    .Select(p => p.Stock)
                    .Sum();

            Console.WriteLine("El Stock total és:{0} ", totalStock);

        }

        public void SelectTotalStock2()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Product>("products");

            var products = collection.AsQueryable().ToList();
            int totalStock = 0;
            foreach (var product in products)
                totalStock += product.Stock;

            Console.WriteLine("Total Stock: " + totalStock);
        }
    }
}
