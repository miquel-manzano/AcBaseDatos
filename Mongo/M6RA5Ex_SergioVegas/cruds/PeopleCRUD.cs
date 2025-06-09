using M6RA5Ex.connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using M6RA5Ex.model;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;

namespace M6RA5Ex.cruds
{
    public class PeopleCRUD
    {

        public void LoadCollection()
        {
            
            const string fileName = "people.json";
            const string filePath = @"..\..\..\files\" + fileName;

            FileInfo fileInfo = new FileInfo(filePath);

            StreamReader sr = fileInfo.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Person> people = NewtonsoftJson.DeserializeObject<List<Person>>(fileString);

            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("people");
            var collection = database.GetCollection<Person>("people");


            try
            {
                if (people != null)
                    collection.InsertMany(people);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection people loaded");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection people couldn't be inserted");
            }
            Console.ResetColor();
        }
        public void DeleteLastTagFromCompany(string company)
        {
            var settings = new JsonWriterSettings { Indent = true };

            var companyFilter = Builders<BsonDocument>.Filter.Eq("company", company);
            var update = Builders<BsonDocument>.Update.PopLast("tags");

            var database = MongoConnection.GetDatabase("itb");
            var people = database.GetCollection<BsonDocument>("people");

            var docToUpdate = people.Find(companyFilter).FirstOrDefault();
            Console.WriteLine($"\n {docToUpdate.ToJson(settings)}");

            people.UpdateOne(companyFilter, update);

            var docUpdated = people.Find(companyFilter).FirstOrDefault();
            Console.WriteLine($"\n {docUpdated.ToJson(settings)}");
        }
        public void SelectTagsCount() 
        {
            var database = MongoConnection.GetDatabase("itb");
            var people = database.GetCollection<BsonDocument>("people");

            var aggregate = people.Aggregate()
                .Unwind("tags")
                .Group(new BsonDocument
                {
                    { "_id", "$tags" },
                    { "TagCount", new BsonDocument("$sum", 1) }
                })
                .Sort(new BsonDocument("count", -1))
                .ToList();

            foreach (var tag in aggregate)
            {
                Console.WriteLine($"\n Tag: {tag}");
            }
        }
        public void SelectFriendNames()
        {
            var database = MongoConnection.GetDatabase("itb");
            var people = database.GetCollection<BsonDocument>("people");

            var aggregate = people.Aggregate()
                            .Unwind("friends") 
                            .Group(new BsonDocument
                                {
                                      { "_id", new BsonDocument("name", "$friends.name") } } 
                                )
                            .Sort(new BsonDocument("_id.name", 1))  
                            .ToList();
            Console.WriteLine("\nLista de nombres únicos de amigos ordenados alfabéticamente:");
            foreach (var friend in aggregate)
            {
                Console.WriteLine($"{friend["_id"]}");
            }
        }
        public void SelectGenderAvergae() 
        {
            var database = MongoConnection.GetDatabase("itb");
            var people = database.GetCollection<BsonDocument>("people");

            var aggregate = people.Aggregate()
                
               .Group(new BsonDocument
               {
                    { "_id", "$gender" },
                    { "count", new BsonDocument("$sum", 1) }
               })
               .Sort(new BsonDocument("count", -1))
               .ToList();
            foreach (var gender in aggregate)
            {
                Console.WriteLine($" Género: {gender["_id"]}, Total: {gender["count"]}");
            }
        }
    }
}
