using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA1.model
{

    [Serializable]
    public class NumberInt
    {
        [JsonProperty("$numberInt")]
        public string _NumberInt { get; set; }
    }
    
}
