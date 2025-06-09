using M6RA5Ex.connections;
using M6RA5Ex.model;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;
using static System.Reflection.Metadata.BlobBuilder;
using System.Threading;

namespace M6RA5Ex.cruds
{
    public class RestaurantsCRUD 
    {
        public  void loadNewRestaurantsObjectCollection()
        {
            Console.WriteLine("Inserint nous  restaurants:");
            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("restaurants");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            FileInfo file = new FileInfo("../../../files/NewRestaurants.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Restaurant restaurant = NewtonsoftJson.DeserializeObject<Restaurant>(line);
                    Console.WriteLine(restaurant?.Name);
                    string json = NewtonsoftJson.SerializeObject(restaurant);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }
        }
        public void LoadCollection()         
        {
            const string fileName = "restaurants.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("restaurants");
            var collection = database.GetCollection<Restaurant>("restaurants");

            using (StreamReader sr = fileInfo.OpenText())
            {
                string restString;
                while ((restString = sr.ReadLine()) != null)
                {
                    Restaurant rest = NewtonsoftJson.DeserializeObject<Restaurant>(restString);
                    collection.InsertOne(rest);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCollection restaurants loaded");
            Console.ResetColor();
        }
        public void InsertRestaurants()
        {

            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Restaurant>("restaurants");

            var restaurant1 = new Restaurant
            {
                Restaurant_id = "9999999559",
                Name = "David Vaquer - El pizzero espagnol",
                Cuisine = "Pizza",
                Borough = "Staten Island",
                Address = new Address
                {
                    Building = "3850",
                    Street = "Richmond Avenue",
                    Zipcode = "10312"
                }
            };

            var restaurant2 = new Restaurant
            {
                Restaurant_id = "99999996691",
                Name = "Joan_Gomez's Hamburgers",
                Cuisine = "American",
                Borough = "Manhattan",
                Address = new Address
                {
                    Building = "24",
                    Street = "East 39 Street",
                    Zipcode = "10016",
                    Coord = new double[] { -73.9812198, 40.7509706 }
                }
            };

            collection.InsertMany(new List<Restaurant> { restaurant1, restaurant2 });

            Console.WriteLine("\nNews restaurants loaded");
        }
        public void ShowMinRatingsSum()
        {

            var database = MongoConnection.GetDatabase("itb");
            var restaurants = database.GetCollection<Restaurant>("restaurants");

            var restaurantSelected =
            restaurants.AsQueryable<Restaurant>()
            .Where(r => r.Grades != null && r.Grades.Any())
            .Select(r => new
            {
                Restaurant = r,
                TotalScore = r.Grades.Sum(g => g.Score)
            }) 
            .OrderBy(r => r.TotalScore) 
            .FirstOrDefault(); 

            Console.WriteLine($"Nom: {restaurantSelected.Restaurant.Name} ");
        }
        public  void SelectAllByBoroughAndNotThatCuisine(string borough, string cuisine) 
        {
            var database = MongoConnection.GetDatabase("itb");
            IMongoCollection<BsonDocument> _collection  = database.GetCollection<BsonDocument>("restaurants");

            var boroughFilter = Builders<BsonDocument>.Filter.Eq("borough", borough);
            var cuisineFilter = Builders<BsonDocument>.Filter.Ne("cuisine", cuisine);

            var filters = Builders<BsonDocument>.Filter.And(boroughFilter, cuisineFilter);
            var restaurantsS = _collection.Find(filters).ToList();

            var settings = new JsonWriterSettings { Indent = true };

            Console.WriteLine();
            foreach (var restaurant in restaurantsS)
            {
                Console.WriteLine($"{restaurant.ToJson(settings)}\n\n");
            }
        }
        public void SelectAllByZipcodeAndCuisine(string zipcode, string cuisine)
        {
            var database = MongoConnection.GetDatabase("itb");
            IMongoCollection<BsonDocument> _collection = database.GetCollection<BsonDocument>("restaurants");

            var boroughFilter = Builders<BsonDocument>.Filter.Eq("address.zipcode", zipcode);
            var cuisineFilter = Builders<BsonDocument>.Filter.Eq("cuisine", cuisine);

            var filters = Builders<BsonDocument>.Filter.And(boroughFilter, cuisineFilter);
            var restaurantsS = _collection.Find(filters).ToList();

            var settings = new JsonWriterSettings { Indent = true };

            Console.WriteLine();
            foreach (var restaurant in restaurantsS)
            {
                Console.WriteLine($"{restaurant.ToJson(settings)}\n\n");
            }
        }
        public void SelectBorough(string borough)
        {

            var database = MongoConnection.GetDatabase("itb");
            var restaurants = database.GetCollection<Restaurant>("restaurants");

            var restaurantSelected =
                restaurants.AsQueryable<Restaurant>()
                .Where(r => r.Borough == borough)
                .ToList();

            foreach (Restaurant restaurant in restaurantSelected)
            {
                Console.WriteLine($"{restaurant.Name}, tipus de cuines : {restaurant.Cuisine}, adreça:{restaurant.Address}");
            }
        }
        public void SelectScore(string restaurant_id)
        {

            var database = MongoConnection.GetDatabase("itb");
            var restaurants = database.GetCollection<Restaurant>("restaurants");

            var restaurantSelected =
                restaurants.AsQueryable<Restaurant>()
                .Where(r => r.Restaurant_id == restaurant_id)
                .Select(r => new
                {
                    GradeCount = r.Grades.Count,
                    ScoreSum = r.Grades.Sum(g=>g.Score)
                })
                .Single();

            Console.WriteLine($"Puntuacio: {restaurantSelected.ScoreSum}, Numero de vegades puntuat : {restaurantSelected.GradeCount} ");
        }
        public void UpdateCoordenadesRestaurant(string id, double coordenades1, double coordenades2)
        {
            var settings = new JsonWriterSettings { Indent = true };
            var database = MongoConnection.GetDatabase("itb");
            var restaurants = database.GetCollection<BsonDocument>("restaurants");

            var idFilter = Builders<BsonDocument>.Filter.Eq("restaurant_id", id);
            var update = Builders<BsonDocument>.Update
                            .Set("address.coord.0", coordenades1)
                            .Set("address.coord.1", coordenades2);

            var restaurantToUpdate = restaurants.Find(idFilter).FirstOrDefault();
            Console.WriteLine($"\n Restaurant a actualitzar: \n --------------------- \n {restaurantToUpdate.ToJson(settings)}");

            var result = restaurants.UpdateOne(idFilter, update);

            var restaurantUpdated = restaurants.Find(idFilter).FirstOrDefault();
            Console.WriteLine($"\n Restaurant actualitzat: \n ------------------- \n {restaurantUpdated.ToJson(settings)}");
        }
        public void SelectBoroughByZipcode()
        {
            var db = MongoConnection.GetDatabase("itb");
            var restaurants = db.GetCollection<BsonDocument>("restaurants");

            var agg = restaurants.Aggregate()
                // 1) sólo documentos con zipcode existente y no vacío
                .Match(Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Exists("address.zipcode", true),
                    Builders<BsonDocument>.Filter.Ne("address.zipcode", BsonNull.Value),
                    Builders<BsonDocument>.Filter.Ne("address.zipcode", "")
                ))
                // 2) agrupa por zipcode y añade los barrios sin repetir
                .Group(new BsonDocument
                {
            { "_id",       "$address.zipcode" },
            { "barrios",   new BsonDocument("$addToSet", "$borough") }
                })
                // 3) ordena por zipcode
                .Sort(Builders<BsonDocument>.Sort.Ascending("_id"))
                .ToList();

            Console.WriteLine("\n Barrios agrupados por código postal:");
            foreach (var doc in agg)
            {
                var zip = doc["_id"].AsString;
                var barrios = doc["barrios"].AsBsonArray.Select(x => x.AsString);
                Console.WriteLine($"Zipcode: {zip} → Barrios: {string.Join(", ", barrios)}");
            }
        }
    }

    /*
      * 
      * 
    public void SelectHigherPrice()
    {

        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<Restaurant>("restaurants");

        var mostExpensiveProduct =
            restaurants.AsQueryable<Restaurant>()
                .OrderByDescending(b => b.Grades)
                .FirstOrDefault();


        Console.WriteLine($"Nom: {mostExpensiveProduct.Name} i preu: {mostExpensiveProduct.Grades}");
    }
    public void SelectBarri(string barri)
    {

        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<Restaurant>("restaurants");

        var restaurantSelected =
            restaurants.AsQueryable<Restaurant>()
            .Where(r => r.Borough == barri )
            .ToList();

        foreach (Restaurant restaurant in restaurantSelected)
        {
            Console.WriteLine($"{restaurant.Name}, tipus de cuines : { restaurant.Cuisine}, adreça:{ restaurant.Address}");
        }
    }
    public void SelectBarriWithoutCuisine( string barri, string cuina)
    {

        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<Restaurant>("restaurants");

        var restaurantSelected =
            restaurants.AsQueryable<Restaurant>()
            .Where(r => r.Borough == barri && !r.Cuisine.Contains(cuina))
            .ToList();

        foreach ( Restaurant restaurant in restaurantSelected)
        {
            Console.WriteLine($"{restaurant.Name}, tipus de cuines : {string.Join(",", restaurant.Cuisine)}");
        }
    }
    public void Selectzipcode(string zipcode, string cuina)
    {
        var database = MongoConnection.GetDatabase("itb");
        var restaurantes = database.GetCollection<BsonDocument>("restaurants");

        var boroughFilter = Builders<BsonDocument>.Filter.Eq("cuisine", cuina);
        var cuisineFilter = Builders<BsonDocument>.Filter.Eq("address.zipcode", zipcode);

        var filters = Builders<BsonDocument>.Filter.And(boroughFilter, cuisineFilter);
        var restaurants = restaurantes.Find(filters).ToList();

        var settings = new JsonWriterSettings { Indent = true };

        Console.WriteLine();
        foreach (var restaurant in restaurants)
        {
            Console.WriteLine($"{restaurant.ToJson(settings)}\n\n");
        }


    }
    public void SelecGradesZipcode(string zipcode)
    {

        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<Restaurant>("restaurants");

        var restaurantSelected =
            restaurants.AsQueryable<Restaurant>()
            .Where(r => r.Address.Zipcode == zipcode)

            .ToList();

        foreach (Restaurant restaurant in restaurantSelected)
        {
            Console.WriteLine($"{restaurant.Name}, zipcode: {restaurant.Address.Zipcode} tipus de cuines : {string.Join(",", restaurant.Cuisine)}");
        }
    }
    public void UpdateCoordenadesRestaurant(string id, double coordenades1, double coordenades2)
    {
        var settings = new JsonWriterSettings { Indent = true };

        var idFilter = Builders<BsonDocument>.Filter.Eq("restaurant_id", id);
        var update = Builders<BsonDocument>.Update.Set("address.coord.[0]", coordenades1);
        var update2 = Builders<BsonDocument>.Update.Set("address.coord.[1]", coordenades2);


        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<BsonDocument>("restaurants");


        var restaurantToUpdate = restaurants.Find(idFilter).FirstOrDefault();
        Console.WriteLine($"\n Restaurant a actualitzar: \n --------------------- \n {restaurantToUpdate.ToJson(settings)}");

        restaurants.UpdateOne(idFilter, update);
        restaurants.UpdateOne(idFilter, update2);

        var restaurantUpdated = restaurants.Find(idFilter).FirstOrDefault();
        Console.WriteLine($"\n Restaurant actualitzat: \n ------------------- \n {restaurantUpdated.ToJson(settings)}");
    }
    public void UpdateZipcodeBronx(string barri, string zipcode)
    {
        var settings = new JsonWriterSettings { Indent = true };

        var idFilter = Builders<BsonDocument>.Filter.Eq("borough", barri);
        var update = Builders<BsonDocument>.Update.Set("address.zipcode", zipcode);


        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<BsonDocument>("restaurants");


        var restaurantToUpdate = restaurants.Find(idFilter).FirstOrDefault();
        Console.WriteLine($"\n Restaurant a actualitzar: \n --------------------- \n {restaurantToUpdate.ToJson(settings)}");

        restaurants.UpdateOne(idFilter, update);

        var restaurantUpdated = restaurants.Find(idFilter).FirstOrDefault();
        Console.WriteLine($"\n Restaurant actualitzat: \n ------------------- \n {restaurantUpdated.ToJson(settings)}");
    }
    public  void DeleteCuisineByBarri(string barri) // EX 3a
    {
        var database = MongoConnection.GetDatabase("itb");
        var restaurants = database.GetCollection<BsonDocument>("restaurants");

        var settings = new JsonWriterSettings { Indent = true };

        var streetFilter = Builders<BsonDocument>.Filter.Eq("borough", barri);
        var update = Builders<BsonDocument>.Update.Unset("cuisine");

        var restaurantToUpdate = restaurants.Find(streetFilter).FirstOrDefault();

        Console.WriteLine($"\n Restaurant a actualitzar: \n ------------------------- \n{restaurantToUpdate.ToJson(settings)}");


        restaurants.UpdateOne(streetFilter, update);

        var restaurantUpdated = restaurants.Find(streetFilter).FirstOrDefault();

        Console.WriteLine($"\n Restaurant actualitzat: \n ----------------------- \n{restaurantUpdated.ToJson(settings)}\n");
    }*/

}


