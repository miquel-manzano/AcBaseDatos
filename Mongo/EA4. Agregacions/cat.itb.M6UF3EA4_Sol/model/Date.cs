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
    public class Date
    {
        [JsonProperty("$date")]
        [BsonElement("date")]
        public long _Date { get; set; }
    }
}
