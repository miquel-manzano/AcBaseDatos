using cat.itb.M6UF3EA4_Sol.connections;
using cat.itb.M6UF3EA4_Sol.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA4_Sol.cruds
{
    public class CountriesCRUD
    {
        private readonly string dbName = "itb";
        private readonly string collectionName = "countries";


        //Ex1
        /**
        * This method is used to load countries collection.
        */
        public void LoadCountriesCollection()
        {
            const string fileName = "countries.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            StreamReader reader = fileInfo.OpenText();
            string fileText = reader.ReadToEnd();
            reader.Close();

            List<Country>? countries = JsonConvert.DeserializeObject<List<Country>>(fileText);

            var db = MongoLocalConnection.GetDatabase("itb");
            db.DropCollection(collectionName);

            Console.WriteLine($"\nColecció {collectionName} eliminada");

            var collection = db.GetCollection<Country>(collectionName);

            try
            {
                if (countries != null)
                    collection.InsertMany(countries);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Tots els registres de la collecció {collectionName} insertats");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();

        }

        //Ex2a
        /**
        * This method is used to obtain English-speaking countries count.
        * @param pLangName Language
        */
        public void GetCountriesLanguageCount(string pLangName)
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Country>(collectionName);

            var filter = Builders<Country>.Filter.Eq("Languages.Name", pLangName);
            var aggregate = collection.Aggregate()
                .Match(filter)
                .Count()
                .Single();

            Console.WriteLine($"Hi ha {aggregate.Count} que parlen {pLangName}");
        }

        //Ex2b
        /**
        * This method is used to obtain the region with the more amount of countries.
        */
        public void GetRegionWithMoreCountries()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Country>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(c => c.Region, g => new { Region = g.Key, Count = g.Count() });

            var result = aggregate.SortByDescending(r => r.Count).Limit(1).FirstOrDefault();

            Console.WriteLine(result.ToString());
        }

        //Ex2b
        /**
        * This method is used to obtain the region with the more amount of countries.
        */
        public void GetRegionWithMoreCountries2()
        {

            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Country>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$region" }, { "numCountries", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "numCountries", -1 } })
                .Limit(1);
            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }

        }

        //Ex2c
        /**
        * This method is used to obtain the amount of countries per subregion.
        */
        public void GetCountriesSubregionCount()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Country>(collectionName);

            var aggregate = collection.Aggregate()
                .Match(c => !string.IsNullOrEmpty(c.Subregion))
                .Group(c => c.Subregion, g => new { Subregion = g.Key, Count = g.Count() });

            foreach (var region in aggregate.ToList())
            {
                Console.WriteLine(region.ToString());
            }
        }

        //Ex2c
        /**
        * This method is used to obtain the amount of countries per subregion.
        */
        public void GetCountriesSubregionCount2()
        {

            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Country>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "$subregion" }, { "numCountries", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "numCountries", -1 } });

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

        //Ex2d
        /**
        * This method is used to obtain the country where the most languages ​​spoken.
        */
        public void GetCountryWithMoreLanguages()
        {
            var db = MongoLocalConnection.GetDatabase(dbName);
            var collection = db.GetCollection<Country>(collectionName);

            var aggregate = collection.Aggregate()
                .Group(c => c.Name, g => new { Country = g.Key, Languages = g.Sum(c => c.Languages.Count) });

            var result = aggregate.SortByDescending(r => r.Languages).Limit(1).FirstOrDefault();

            Console.WriteLine(result.ToString());
        }

        //Ex2d
        /**
        * This method is used to obtain the country where the most languages ​​spoken.
        */
        public void GetCountryWithMoreLanguages2()
        {
            var database = MongoLocalConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Country>(collectionName);
            var aggregate = collection.Aggregate()
                .Unwind("Languages")
                .Group(new BsonDocument { { "_id", "$name" }, { "numLang", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "numLang", -1 } })
                .Limit(1);

            var results = aggregate.ToList();
            foreach (var obj in results)
            {
                Console.WriteLine(obj.ToString());
            }
        }

    }
}
