using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UF3_test.connections;
using UF3_test.model;

namespace UF3_test.cruds
{
    public class ProductsCrud
    {
        public void SelectMoreExpensiveProduct()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Product>("products");

            var query = booksCollection.AsQueryable<Product>();

            var maxPrice = query
                    .Select(p => p.Price)
                    .Max();
            var product = query
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

            var totalStock = query
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
