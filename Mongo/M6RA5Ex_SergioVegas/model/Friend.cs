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
    public class Friend
    {
        [JsonProperty("id")]
        [BsonElement("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
