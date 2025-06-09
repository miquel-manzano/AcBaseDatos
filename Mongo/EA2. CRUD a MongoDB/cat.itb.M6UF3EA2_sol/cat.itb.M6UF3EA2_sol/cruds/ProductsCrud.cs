using System;
using System.IO;
using cat.itb.M6UF3EA2_sol.connections;
using cat.itb.M6UF3EA2_sol.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_sol.cruds
{
    public class ProductsCrud
    {
        public void LoadProductsCollection()
        { 
            FileInfo file = new FileInfo("../../../files/products.json");
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");            

            using (StreamReader sr = file.OpenText())
            {
                string productString;
                while ((productString = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(productString);
                    Console.WriteLine(product?.Name);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(JsonConvert.SerializeObject(product)));
                    collection.InsertOne(document);
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCollection products loaded");
            Console.ResetColor();
        }

        public void UpdateProductsAddNewFieldWherePriceGreaterThan(string pField, int pValue, int pPrice)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Gt("price", pPrice);
            var update = Builders<BsonDocument>.Update.Set(pField, pValue);

            var docsUpdated = collection.UpdateMany(filter, update);
            var result = collection.Find(filter).ToList();
            Console.WriteLine($"\nProductes amb preu superior a {pPrice} actualitzats:");
            foreach (var doc in result)
            {
                Console.WriteLine(doc);
            }
            Console.WriteLine("Productes actualitzats: " + docsUpdated.ModifiedCount);
        }

        public void UpdateProductsAddGammaField()
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("products");

            var result = collection.Find(new BsonDocument()).ToList();

            foreach (var doc in result)
            {
                var price = doc["price"].AsInt32;
                string value = "";

                if (price >= 1 && price <= 500) value = "baixa";
                else if (price >= 501 && price <= 2000) value = "mitja";
                else if (price > 2000) value = "extra";

                var filter = Builders<BsonDocument>.Filter.Eq("_id", doc["_id"]);
                var update = Builders<BsonDocument>.Update.Set("gamma", value);
                collection.UpdateOne(filter, update);
            }
            Console.WriteLine("El camp gamma ha estat afegit a tots els productes");
        }

        public void UpdateProductCategoryByName(string pName, string pCategory, string pNewCategory)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("products");

            var searchFilter = Builders<BsonDocument>.Filter.Eq("name", pName);
            var filter = Builders<BsonDocument>.Filter.Eq("name", pName) & Builders<BsonDocument>.Filter.Eq("categories", pCategory);
            var update = Builders<BsonDocument>.Update.Set("categories.$", pNewCategory);
            var result = collection.Find(searchFilter).FirstOrDefault();

            Console.WriteLine($"\nProducte amb nom {pName} ABANS de l'actualització:");
            Console.WriteLine(result);

            collection.UpdateMany(filter, update);
            result = collection.Find(searchFilter).FirstOrDefault();
            Console.WriteLine($"\nProducte amb nom {pName} DESPRÉS de l'actualització:");
            Console.WriteLine(result);
        }
        public void UpdateStockWherePriceBetween(int pPrice1, int pPrice2, int pNewStock)
        {
            if (pPrice1 > pPrice2) (pPrice1, pPrice2) = (pPrice2, pPrice1);
            
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Gte("price", pPrice1);
            filter &= Builders<BsonDocument>.Filter.Lte("price", pPrice2);
            var update = Builders<BsonDocument>.Update.Set("stock", pNewStock);
            var docsUpdated = collection.UpdateMany(filter, update);
            
            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);

            if (docsUpdated.ModifiedCount != 0)
            {
                Console.WriteLine("Productes modificats:");
                var products = collection.Find(filter).ToList();
                foreach (var product in products)
                    Console.WriteLine(product);
            }
        }

   
        /*
        public void SelectStockGtAndAddFieldDiscount(int stock)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Gt("stock", stock);
            var update = Builders<BsonDocument>.Update.Set("discount", 0.20);
            var docsUpdated = collection.UpdateMany(filter, update);
            
            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);

            if (docsUpdated.ModifiedCount != 0)
            {
                Console.WriteLine("Productes modificats:");
                var products = collection.Find(filter).ToList();
                foreach (var product in products)
                    Console.WriteLine("     " + product.GetElement("name"));
            }
        }
        */

     
        /*
        public void AddCategoryToProduct(string name)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var update = Builders<BsonDocument>.Update.Push("categories", "smartTV");
            var docsUpdated = collection.UpdateOne(filter, update);
            
            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);

            if (docsUpdated.ModifiedCount != 0)
            {
                Console.WriteLine("Productes modificats:");
                var product = collection.Find(filter).FirstOrDefault();
                Console.WriteLine("     " + product.GetElement("name"));
            }
        }
        */
       
        
     /*
        public void DeleteProductsWithPriceBetween(int price1, int price2)
        {
            if (price2 > price1) (price1, price2) = (price2, price1);
            
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var deleteFilter = Builders<BsonDocument>.Filter.Lte("price", price1);
            deleteFilter &= Builders<BsonDocument>.Filter.Gte("price", price2);
            
            var docsDeleted = collection.DeleteMany(deleteFilter);
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);
        }
     */

        public void DeleteProductWithNameEq(string pName)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("name", pName);
            
            var docsDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);
        }
        
        public void DeleteFirstCategoryFromProductWithName(string pName)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("name", pName);
            var update = Builders<BsonDocument>.Update.PopFirst("categories");

            var productB = collection.Find(filter).First();
            Console.WriteLine("Primera categoria de " + productB.GetElement("name") + " per eliminar");
            Console.WriteLine(productB.GetElement("categories"));

            collection.UpdateOne(filter, update);

            var productA = collection.Find(filter).First();
            Console.WriteLine("Primera categoria de " + productA.GetElement("name") + " eliminada");
            Console.WriteLine(productA.GetElement("categories"));
        }

        public void DeleteProductsByCategory(string pCategory)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("categories", pCategory);
            var docsDeleted = collection.DeleteMany(filter);

            Console.WriteLine($"Productes amb categoria {pCategory} eliminats");
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);
        }
    }
}