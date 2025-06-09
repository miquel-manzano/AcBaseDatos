using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA1.model
{
    [Serializable]
    public class Date
    {
        [JsonProperty("$date")] 
        public long _Date { get; set; }
    }
}