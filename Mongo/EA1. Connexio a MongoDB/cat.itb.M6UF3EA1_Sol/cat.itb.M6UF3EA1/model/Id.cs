using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA1.model
{
    [Serializable]
    public class Id
    {
        [JsonProperty("$oid")] 
        public string Oid { get; set; }
    }
}