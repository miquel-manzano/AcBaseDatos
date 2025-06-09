using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UF3_test.connections;

namespace UF3_test.cruds
{
    public class RestaurantsCrud
    {
        public void SelectByBoroughAndCuisinePrintAllData(string pBorough, string pCuisine)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("borough", pBorough),
                Builders<BsonDocument>.Filter.Eq("cuisine", pCuisine)
            );

            /*var filter = Builders<BsonDocument>.Filter.Eq("borough", pBorough);
            filter &= Builders<BsonDocument>.Filter.Eq("cuisine", pCuisine);*/
            var restaurants = collection.Find(filter).FirstOrDefault();
            foreach (var restaurant in restaurants)
                Console.WriteLine(restaurant);
        }

        public void SelectByZipcodePrintNameAndCuisine(string pZipcode)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("address.zipcode", pZipcode);
            var restaurants = collection.Find(filter).ToList();
            foreach (var restaurant in restaurants)
            {
                var restaurantElement = restaurant.GetValue("name");
                var restaurantCuisine = restaurant.GetValue("cuisine");
                Console.WriteLine("Nom: " + restaurantElement);
                Console.WriteLine("Cuina: " + restaurantCuisine);
            }
        }

        public void UpdateZipcodeWhereStreetEq(String pStreet, String pZipcode)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("address.street", pStreet);
            var update = Builders<BsonDocument>.Update.Set("address.zipcode", pZipcode);


            Console.WriteLine("Restaurants per modificar:");
            var restsB = collection.Find(filter).ToList();
            foreach (var restB in restsB)
            {
                Console.WriteLine(restB);
            }

            var docsUpdated = collection.UpdateMany(filter, update);

            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);

            if (docsUpdated.ModifiedCount != 0)
            {
                Console.WriteLine("Productes modificats:");
                var restsA = collection.Find(filter).ToList();
                foreach (var restA in restsA)
                    Console.WriteLine(restA);
            }

            //Si volem veure només el nom del Restaurant, el carrer i els codi postal haurem de fer projeccions
            //var projection = Builders<BsonDocument>.Projection.Include("address.zipcode");
        }

        /*
        public void AddFieldStarsWhereCuisineEq(string cuisine)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("cuisine", cuisine);
            var update = Builders<BsonDocument>.Update.Set("stars", "*****");
            var docsUpdated = collection.UpdateMany(filter, update);
            
            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);
            
            if (docsUpdated.ModifiedCount != 0)
            {
                Console.WriteLine("Productes modificats:");
                var products = collection.Find(filter).ToList();
                foreach (var product in products)
                    Console.WriteLine(product + "\n");
            }
        }
        */

        public void DeleteRestaurantsFromBorough(string pBorough)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("borough", pBorough);

            var docsDeleted = collection.DeleteMany(deleteFilter);
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);
        }
    }
}
