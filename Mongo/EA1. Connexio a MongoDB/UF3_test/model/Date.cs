using System;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Date
    {
        [JsonProperty("$date")] 
        public long _Date { get; set; }
    }
}