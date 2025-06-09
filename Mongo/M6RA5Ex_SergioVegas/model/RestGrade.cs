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
    public class RestGrade
    {
        [JsonProperty("date")]
        [BsonElement("date")]
        public Date Date { get; set; }

        [JsonProperty("grade")]
        [BsonElement("grade")]
        public String Grade { get; set; }

        [JsonProperty("score")]
        [BsonElement("score")]
        public int Score { get; set; }
    }
}
