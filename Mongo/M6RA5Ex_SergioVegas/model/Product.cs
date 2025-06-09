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
    public class Product
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        [BsonElement("price")]
        public int Price { get; set; }

        [JsonProperty("stock")]
        [BsonElement("stock")]
        public int Stock { get; set; }

        [JsonProperty("picture")]
        [BsonElement("picture")]
        public string Picture { get; set; }

        [JsonProperty("categories")]
        [BsonElement("categories")]
        public List<string> Categories { get; set; }
    }
}
