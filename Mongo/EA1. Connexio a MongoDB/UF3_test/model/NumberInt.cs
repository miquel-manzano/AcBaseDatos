using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{

    [Serializable]
    public class NumberInt
    {
        [JsonProperty("$numberInt")]
        public string _NumberInt { get; set; }
    }
    
}
