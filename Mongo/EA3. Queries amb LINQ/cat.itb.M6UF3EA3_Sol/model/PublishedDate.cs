using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA3_Sol.model
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
                "$Date = '" + Date + '\'' +
                "}";
        }
    }
}
