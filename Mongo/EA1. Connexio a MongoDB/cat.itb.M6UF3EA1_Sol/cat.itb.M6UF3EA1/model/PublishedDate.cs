using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA1.model
{
    [Serializable]
    public class PublishedDate
    {
        [JsonProperty("$date")] 
        public String Date { get; set; }
    
        public override string ToString()
        {
            return 
                "PublishedDate{" + 
                "$_Date = '" + Date + '\'' + 
                "}";
        }
    }
}