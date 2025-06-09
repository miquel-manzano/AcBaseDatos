using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Currency
    {
        [JsonProperty("code")]
        [BsonElement("code")]
        public String Code { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public String Name { get; set; }

        [JsonProperty("symbol")]
        [BsonElement("symbol")]
        public String Symbol { get; set; }
    }
}
