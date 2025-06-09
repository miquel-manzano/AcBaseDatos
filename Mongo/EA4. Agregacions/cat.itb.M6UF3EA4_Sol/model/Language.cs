using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA4_Sol.model
{
    [Serializable]
    public class Language
    {
        [JsonProperty("iso639_1")]
        [BsonElement("iso639_1")]
        public String Iso639_1 { get; set; }

        [JsonProperty("iso639_2")]
        [BsonElement("iso639_2")]
        public String Iso639_2 { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public String Name { get; set; }

        [JsonProperty("nativeName")]
        [BsonElement("nativeName")]
        public String NativeName { get; set; }
    }
}
