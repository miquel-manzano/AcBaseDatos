using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace M6RA5Ex.model
{
    [Serializable]
    public class Person
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("isActive")]
        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("balance")]
        [BsonElement("balance")]
        public string Balance { get; set; }

        [JsonProperty("picture")]
        [BsonElement("picture")]
        public string Picture { get; set; }

        [JsonProperty("age")]
        [BsonElement("age")]
        public int Age { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("company")]
        [BsonElement("company")]
        public string Company { get; set; }

        [JsonProperty("phone")]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        [BsonElement("email")]
        public string Email { get; set; }

        [JsonProperty("address")]
        [BsonElement("address")]
        public string Address { get; set; }

        [JsonProperty("about")]
        [BsonElement("about")]
        public string About { get; set; }

        [JsonProperty("registered")]
        [BsonElement("registered")]
        public string Registered { get; set; }

        [JsonProperty("latitude")]
        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("tags")]
        [BsonElement("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("friends")]
        [BsonElement("friends")]
        public List<Friend> Friends { get; set; }

        [JsonProperty("gender")]
        [BsonElement("gender")]
        public string Gender { get; set; }

        [JsonProperty("randomArrayItem")]
        [BsonElement("randomArrayItem")]
        public string RandomArrayItem { get; set; }
    }
}
