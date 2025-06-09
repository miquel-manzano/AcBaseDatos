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
    public class Date
    {
        [JsonProperty("$date")]
        [BsonElement("date")]
        public long _Date { get; set; }
    }
}
