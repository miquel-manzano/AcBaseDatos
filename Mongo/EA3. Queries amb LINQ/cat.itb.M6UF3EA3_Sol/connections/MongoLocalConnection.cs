using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA3_Sol.connections
{
    public class MongoLocalConnection
    {
        private static String URL = "mongodb://127.0.0.1:27017/";

        public static IMongoDatabase GetDatabase(String database)
        {
            MongoClient dbClient = new MongoClient(URL);
            return dbClient.GetDatabase(database);
        }

        public static MongoClient GetMongoClient()
        {
            return new MongoClient(URL);
        }
    }
}
