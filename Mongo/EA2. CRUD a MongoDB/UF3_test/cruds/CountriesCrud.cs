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
    public class CountriesCrud
    {
        public void CountPopulationByRegion(string pRegion)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("region", pRegion);
            var result = collection.Find(filter).ToList();

            int totalPopulation = 0;

            foreach (var doc in result)
            {
                totalPopulation += doc.GetElement("population").Value.ToInt32();
            }
            Console.WriteLine($"La població total de la regió {pRegion} és: {totalPopulation}");
        }

        public void SelectCountryCapitalPopulationLatLngByName(string pName)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("name", pName);
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine($"País: {pName}, Capital: {result.GetElement("capital").Value}, Població: {result.GetElement("population").Value}, LatLng: {result.GetElement("latlng").Value}");
            }
            else
            {
                Console.WriteLine("No existeix el country" + pName);
            }
        }

        public void UpdateCountryCallingCodesByName(string pName, string pCallingCode)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("name", pName);
            var update = Builders<BsonDocument>.Update.Push("callingCodes", pCallingCode);

            var result = collection.Find(filter).FirstOrDefault();
            Console.WriteLine("\nPaís ABANS de l'actualització");
            Console.WriteLine(result);

            collection.UpdateOne(filter, update);
            result = collection.Find(filter).FirstOrDefault();
            Console.WriteLine("\nPaís DESPRÉS de l'actualització");
            Console.WriteLine(result);
        }

        public void DeleteCountriesBySpeakingLanguage(string pLanguage)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("languages.name", pLanguage);
            var result = collection.DeleteMany(filter);

            Console.WriteLine($"S'han eliminat els països que parlen {pLanguage}");

            Console.WriteLine($"Documents eliminats: {result.DeletedCount}");
        }
    }
}
