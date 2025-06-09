using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    [Serializable]
    public class NumberDouble
    {
        [JsonProperty("$numberDouble")]
        public string _NumberDouble { get; set; }
    }
    
}
