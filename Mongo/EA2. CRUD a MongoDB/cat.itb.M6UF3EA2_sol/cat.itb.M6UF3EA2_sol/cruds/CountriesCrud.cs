using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using cat.itb.M6UF3EA2_sol.connections;
using cat.itb.M6UF3EA2_sol.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_sol.cruds
{
    public class CountriesCrud
    {
        public void LoadCountriesCollection()
        {
            FileInfo file = new FileInfo("../../../files/countries.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("countries");
            var collection = database.GetCollection<BsonDocument>("countries");

            try
            {
                if (countries != null)
                foreach (var country in countries)
                {
                    Console.WriteLine(country.Name);
                    string json = JsonConvert.SerializeObject(country);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json)); ;
                    collection.InsertOne(document);
                }

              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine("\nCollection countries loaded");
            }
            catch
            {
             Console.ForegroundColor = ConsoleColor.Red;
             Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();
        }

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
