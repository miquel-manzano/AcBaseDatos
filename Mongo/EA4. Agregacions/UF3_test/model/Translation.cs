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
    public class Translation
    {
        [JsonProperty("de")]
        [BsonElement("de")]
        public String De { get; set; }

        [JsonProperty("es")]
        [BsonElement("es")]
        public String Es { get; set; }

        [JsonProperty("fr")]
        [BsonElement("fr")]
        public String Fr { get; set; }

        [JsonProperty("ja")]
        [BsonElement("ja")]
        public String Ja { get; set; }

        [JsonProperty("it")]
        [BsonElement("it")]
        public String It { get; set; }

        [JsonProperty("br")]
        [BsonElement("br")]
        public String Br { get; set; }

        [JsonProperty("pt")]
        [BsonElement("pt")]
        public String Pt { get; set; }

        [BsonElement("nl")]
        [JsonProperty("nl")]
        public String Nl { get; set; }

        [BsonElement("hr")]
        [JsonProperty("hr")]
        public String Hr { get; set; }

        [JsonProperty("fa")]
        [BsonElement("fa")]
        public String Fa { get; set; }
    }
}
