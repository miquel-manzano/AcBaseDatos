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
    public class RestaurantGrade
    {
        [JsonProperty("date")]
        [BsonElement("date")]
        public Date Date { get; set; }

        [JsonProperty("grade")]
        [BsonElement("grade")]
        public string Grade { get; set; }

        [JsonProperty("score")]
        [BsonElement("score")]
        public int Score { get; set; }
    }
}
