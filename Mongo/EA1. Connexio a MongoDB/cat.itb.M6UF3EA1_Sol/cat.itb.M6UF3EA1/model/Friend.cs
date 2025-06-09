using Newtonsoft.Json;
using System;

namespace cat.itb.M6UF3EA1.model

{
    [Serializable]
    public class Friend
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

