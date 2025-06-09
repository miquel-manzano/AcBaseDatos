using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Restaurant
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("address")]
        [BsonElement("address")]
        public Address Address { get; set; }

        [JsonProperty("borough")]
        [BsonElement("borough")]
        public string Borough { get; set; }

        [JsonProperty("cuisine")]
        [BsonElement("cuisine")]
        public string Cuisine { get; set; }

        [JsonProperty("grades")]
        [BsonElement("grades")]
        public List<RestaurantGrade> Grades { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("restaurant_id")]
        [BsonElement("restaurant_id")]
        public string Restaurant_id { get; set; }
    }
}
