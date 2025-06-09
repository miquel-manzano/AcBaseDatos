using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA4_Sol.model
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
