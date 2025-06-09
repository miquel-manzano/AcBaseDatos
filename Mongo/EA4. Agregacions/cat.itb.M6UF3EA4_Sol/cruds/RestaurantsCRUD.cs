using cat.itb.M6UF3EA4_Sol.connections;
using cat.itb.M6UF3EA4_Sol.model;
using Newtonsoft.Json;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace cat.itb.M6UF3EA4_Sol.cruds
{
    public class RestaurantsCRUD
    {
        private readonly string dbName = "itb";
        private readonly string collectionName = "restaurants";

        //Ex1
        /**
        * This method is used to load restaurants collection.
        */
        public void LoadRestaurantsCollection()
        {
            const string fileName = "restaurants.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            var db = MongoLocalConnection.GetDatabase(dbName);
            db.DropCollection(collectionName);

            Console.WriteLine($"\nColecció {collectionName} eliminada");

            var collection = db.GetCollection<Restaurant>(collectionName);

            using (StreamReader reader = fileInfo.OpenText())
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Restaurant? register = JsonConvert.DeserializeObject<Restaurant>(line);
                    if (register != null) collection.InsertOne(register);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Tots els registres de la collecció {collectionName} insertats");
            Console.ResetColor();

        }
                
        //Ex2e
        /**
        * This method is used to obtain the scores count by grade 
        */
        public void GetRestaurantsGradesScoreCount()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);
            var aggregate = collection.Aggregate()
                .Unwind("grades")
                .Group(new BsonDocument { { "_id", "$grades.score" }, { "num", new BsonDocument("$sum", 1) } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        //Ex2e
        /**
        * This method is used to obtain the scores count by grade
        */
        public void GetRestaurantsGradesScoreCount2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);

            var result = collection.Aggregate()
                .Unwind("grades")
                .Group(new BsonDocument { { "_id", "$grades.score" }, { "count", new BsonDocument("$sum", 1) } })
                .ToList();

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in result)
            {
                Console.WriteLine(item["_id"] + " - " + item["count"]);
            }
            Console.ResetColor();
            Console.ReadKey();
        }

        //Ex2f
        /**
        * This method is used to obtain the zipcodes by borough 
        */
        public void GetZipcodesByBorough()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(r => r.Borough, g => new { Borough = g.Key, PostalCodes = g.Select(r => r.Address.Zipcode).Distinct() });

            foreach (var borough in aggregate.ToList())
            {
                Console.WriteLine($"{borough.Borough}:");
                foreach (var postalCode in borough.PostalCodes)
                {
                    Console.Write($"{postalCode}, ");
                }
                Console.WriteLine("\n");
            }
        }

        //Ex2f
        /**
        * This method is used to obtain the zipcodes by borough 
        */
        public void GetZipcodesByBorough2()
              {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);
            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$borough" }, { "zipcodes", new BsonDocument("$addToSet", "$address.zipcode") } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
                Console.WriteLine();
            }
        }

        //Ex2f
        /**
        * This method is used to obtain the zipcodes by borough 
        */
        public void GetZipcodesByBorough3()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);

            var result = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$borough" }, { "zipcodes", new BsonDocument("$addToSet", "$address.zipcode") } })
                .ToList();

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in result)
            {
                Console.WriteLine("Barri: " + item["_id"]);
                Console.WriteLine("Codis postals:");
                foreach (var zip in item["zipcodes"].AsBsonArray)
                {
                    Console.WriteLine("\t" + zip);
                }
            }
            Console.ResetColor();
            Console.ReadKey();
        }

        //Ex2g
        /**
        * This method is used to obtain restaurants count by cuisine 
        */
        public void GetRestaurantsCuisineCount()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(r => r.Cuisine, g => new { Cuisine = g.Key, Count = g.Count() });
            var results = aggregate.ToList().OrderByDescending(c => c.Count);

            foreach (var cuisine in results)
            {
                Console.WriteLine(cuisine.ToString());
            }
        }

        //Ex2g
        /**
        * This method is used to obtain restaurants count by cuisine  
        */
        public void GetRestaurantsCuisineCount2()
        {

            var database = MongoLocalConnection.GetDatabase(dbName);
            var booksCollection = database.GetCollection<Restaurant>(collectionName);

            var aggregate = booksCollection.Aggregate()
                .Group(new BsonDocument { { "_id", "$cuisine" }, { "count", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "count", -1 } });
            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        //Ex2h
        /**
        * This method is used to count grades by restaurants 
        */
        public void GetRestaurantsGradesCount()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(r => r.Restaurant_id, g => new { Restaurant_id = g.Key, Count = g.Sum(r => r.Grades.Count) });

            foreach (var restaurant in aggregate.ToList())
            {
                Console.WriteLine(restaurant.ToString());
            }
        }

        //Ex2h
        /**
        * This method is used to count grades by restaurants
        */
        public void GetRestaurantsGradesCount2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);
            var aggregate = collection.Aggregate()
                .Unwind("grades")
                .Group(new BsonDocument { { "_id", "$name" }, { "numGrades", new BsonDocument("$sum", 1) } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        //Ex2i
        /**
        * This method is used to obtain the different cuisines type by borough 
        */
        public void GetCuisinesByBorough()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(r => r.Borough, g => new { Borough = g.Key, Cuisines = g.Select(r => r.Cuisine).Distinct() });

            foreach (var borough in aggregate.ToList())
            {
                Console.WriteLine($"{borough.Borough}:");
                foreach (var cuisine in borough.Cuisines)
                {
                    Console.Write($"{cuisine}, ");
                }
                Console.WriteLine("\n");
            }
        }

        //Ex2i
        /**
        * This method is used to obtain the different cuisines type by borough  
        */
        public void GetCuisinesByBorough2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);
            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$borough" }, { "cuisineTypes", new BsonDocument("$addToSet", "$cuisine") } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
                Console.WriteLine();
            }
        }

        //Ex2j
        /**
        * This method is used to obtain the high score by restaurant 
        */
        public void GetHighScoreByRestaurant()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(r => r.Name, g => new { Name = g.Key, MaxScore = g.Max(g => g.Grades.Select(gr => gr.Score)).Max() });

            var results = aggregate.ToList().OrderByDescending(c => c.MaxScore);

            foreach (var restaurant in results.ToList())
            {
                Console.WriteLine(restaurant.ToString());
            }
        }

        //Ex2j
        /**
        * This method is used to obtain the high score by restaurant
        */
        public void GetHighScoreByRestaurant2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Restaurant>(collectionName);

            var aggregate = collection.Aggregate()
                .Unwind("grades")
                .Group(new BsonDocument { { "_id", "$name" }, { "highscore", new BsonDocument("$max", "$grades.score") } })
                .Sort(new BsonDocument { { "highscore", -1 } });            

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }
    }
}
