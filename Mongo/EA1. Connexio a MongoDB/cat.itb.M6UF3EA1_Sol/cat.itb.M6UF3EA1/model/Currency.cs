using Newtonsoft.Json;
using System;

namespace cat.itb.M6UF3EA1.model
{
    public class Currency
    {
        [JsonProperty("code")]
        public String Code { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("symbol")]
        public String Symbol { get; set; }
    }
}
