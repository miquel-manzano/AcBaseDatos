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
    public class RegionalBloc
    {
        [JsonProperty("acronym")]
        [BsonElement("acronym")]
        public String Acronym { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public String Name { get; set; }

        [JsonProperty("otherAcronyms")]
        [BsonElement("otherAcronyms")]
        public List<String> OtherAcronyms { get; set; }

        [JsonProperty("otherNames")]
        [BsonElement("otherNames")]
        public List<String> OtherNames { get; set; }
    }
}
