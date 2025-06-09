using System;
using System.Collections.Generic;
using System.IO;
using cat.itb.M6UF3EA2_sol.connections;
using cat.itb.M6UF3EA2_sol.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace cat.itb.M6UF3EA2_sol.cruds
{
    public class PeopleCrud
    {
        public void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            string fileString = file.OpenText().ReadToEnd();
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("people");
            var collection = database.GetCollection<BsonDocument>("people");

            try
            {
                if (people != null)
                    foreach (var person in people)
                    {
                        Console.WriteLine(person.Name);
                        var document = new BsonDocument();
                        document.AddRange(BsonDocument.Parse(JsonConvert.SerializeObject(person)));
                        collection.InsertOne(document);
                    }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection people loaded");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();            
        }

        public void SelectByNamePrintFriendsName(string pName)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = Builders<BsonDocument>.Filter.Eq("name", pName);

            //Si agafem tots els documents Friend
            //var person = collection.Find(filter).FirstOrDefault();
            //var friends = person.GetElement("friends");
            //Console.WriteLine(friends);

            //Si fem una projecció d'un camp del document
            var projection = Builders<BsonDocument>.Projection.Include("friends.name");
            var result = collection.Find(filter).Project(projection).ToList();

            foreach (var doc in result)
            {
                Console.WriteLine($"Amics de {pName}: {doc["friends"]}");
            }

        }

        public void DeleteFieldByRandomArrayItem(string pField, string pItem)
        {
            var db = MongoLocalConnection.GetDatabase("itb");
            var collection = db.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("randomArrayItem", pItem);
            var update = Builders<BsonDocument>.Update.Unset(pField);

            var result = collection.Find(filter).ToList();
            Console.WriteLine($"\nDocs ABANS de l'actualització:");
            foreach (var doc in result)
            {
                Console.WriteLine(doc);
            }

            collection.UpdateMany(filter, update);

            Console.WriteLine($"El camp {pField} ha estat eliminat dels documents amb RandomArrayItem = {pItem}");

            result = collection.Find(filter).ToList();
            Console.WriteLine($"\nDocs DESPRÉS de l'actualització:");
            foreach (var doc in result)
            {
                Console.WriteLine(doc);
            }            
        }
    }
}