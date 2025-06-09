using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UF3_test.model
{
    [Serializable]
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public String Picture { get; set; }
        public List<String> Categories { get; set; }
    }
}