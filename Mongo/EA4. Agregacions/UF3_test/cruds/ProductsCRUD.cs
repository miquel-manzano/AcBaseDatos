using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;

namespace UF3_test.cruds
{
    public class ProductsCRUD
    {
        private readonly string dbName = "itb";
        private readonly string collectionName = "products";

        //Ex1
        /**
        * This method is used to load products collection.
        */
        public void LoadProductsCollection()
        {
            const string fileName = "products.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            var db = MongoLocalConnection.GetDatabase(dbName);
            db.DropCollection(collectionName);

            Console.WriteLine($"\nColecció {collectionName} eliminada");

            var collection = db.GetCollection<Product>(collectionName);

            using (StreamReader reader = fileInfo.OpenText())
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Product? register = JsonConvert.DeserializeObject<Product>(line);
                    if (register != null) collection.InsertOne(register);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Tots els registres de la collecció {collectionName} insertats");
            Console.ResetColor();
        }

        //Ex2k
        /**
        * This method is used to count the categories product
        */
        public void GetCategoriesProductCount()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Product>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(p => p.Name, g => new { Name = g.Key, Count = g.Sum(p => p.Categories.Count) });

            foreach (var product in aggregate.ToList())
            {
                Console.WriteLine(product.ToString());
            }
        }

        //Ex2k
        /**
        * This method is used to count the categories product
        */
        public void GetCategoriesProductCount2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Product>(collectionName);
            var aggregate = collection.Aggregate()
                .Unwind("Categories")
                .Group(new BsonDocument { { "_id", "$name" }, { "numCats", new BsonDocument("$sum", 1) } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        //Ex2l
        /**
        * This method is used to obtain all distinct categories
        */
        public void GetAllCategories()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Product>(collectionName);
            var aggregate = collection.Aggregate()
                .Unwind("categories")
                .Group(new BsonDocument { { "_id", "$categories" } })
                .ToList();

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in aggregate)
            {
                Console.WriteLine(item["_id"]);
            }
            Console.ResetColor();
            Console.ReadKey();

        }
    }
}
