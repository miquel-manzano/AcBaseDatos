using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace M6RA5Ex.model
{
    [Serializable]
    public class Address
    {
        [JsonProperty("building")]
        [BsonElement("building")]
        public String Building { get; set; }

        [JsonProperty("coord")]
        [BsonElement("coord")]
        public double[] Coord { get; set; }

        [JsonProperty("street")]
        [BsonElement("street")]
        public String Street { get; set; }

        [JsonProperty("zipcode")]
        [BsonElement("zipcode")]
        public String Zipcode { get; set; }
    }
}
