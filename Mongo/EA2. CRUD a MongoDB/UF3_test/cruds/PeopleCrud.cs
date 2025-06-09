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
    public class PeopleCrud
    {
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
