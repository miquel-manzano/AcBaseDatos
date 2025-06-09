using M6RA5Ex.connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M6RA5Ex.model;
using Newtonsoft.Json;
using M6RA5Ex.model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace M6RA5Ex.cruds
{
    public class ProductsCRUD
    {
        public void LoadCollection()
        {

            const string fileName = "products.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<Product>("products");

            using (StreamReader sr = fileInfo.OpenText())
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
        public void SelectCategoryMinStock(int stockMinim, string categoria)
        {

            var database = MongoConnection.GetDatabase("itb");
            var products = database.GetCollection<Product>("products");

            var productSelected =
                products.AsQueryable<Product>()
                .Where(r => r.Categories.Contains(categoria) && r.Stock < stockMinim)
                .ToList();

            foreach (Product product in productSelected)
            {
                Console.WriteLine($"{product.Name}, preu: {product.Price} i stock: {product.Stock}");
            }
        }
        public void SelectAveragePrice()
        {
            var database = MongoConnection.GetDatabase("itb");
            var products = database.GetCollection<Product>("products");



            var query = products.AsQueryable<Product>();

            var prices = query.Select(p => p.Price);
            var priceAverage = prices.Average();

            Console.WriteLine($"\n Mitja de preus: {priceAverage}");
        }
        public void RemoveLowStockProducts()
        {
            var database = MongoConnection.GetDatabase("itb");
            var products = database.GetCollection<Product>("products");

            var filter = Builders<Product>.Filter.Lt(p => p.Stock, 60);

            var result = products.DeleteMany(filter); 

            Console.WriteLine($"{result.DeletedCount} productos eliminados por bajo stock.");
        }
        public void DeletedCategory(string category)
        {
            var database = MongoConnection.GetDatabase("itb");
            var products = database.GetCollection<BsonDocument>("products");

            var catFilter = Builders<BsonDocument>.Filter.Eq("categories", category);

            var docsDeleted = products.DeleteMany(catFilter);
            Console.WriteLine($"\n Documents eliminats: {docsDeleted.DeletedCount}");
        }
    }
}
