using System;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Id
    {
        [JsonProperty("$oid")] 
        public string Oid { get; set; }
    }
}