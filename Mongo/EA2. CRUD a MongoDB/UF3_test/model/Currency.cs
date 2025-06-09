using Newtonsoft.Json;
using System;

namespace UF3_test.model
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
